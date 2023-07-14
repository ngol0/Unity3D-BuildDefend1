using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameplayController : MonoBehaviour
{
    [Header("Layer Mask for Raycast")]
    [SerializeField] LayerMask interactableMask;
    [SerializeField] LayerMask gridMask;

    [Header("Inventory Data")]
    public InventorySO inventory;

    [Header("Grid Ref")]
    [SerializeField] PlayGrid playGrid;
    [SerializeField] Pathfinding pathFindingGrid;

    [Header("Event to raise")]
    [SerializeField] InteractableEvent OnItemSelected;
    public System.Action OnItemPlaced;
    public System.Action OnCancelPlacedItem;

    InteractableItem selectedItem;
    InteractableData itemToPlaceData;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (InteractWithUI()) return;
            if (TryPlaceItemAtGrid()) return;

            TrySelectItem();
        }
    }

    private void TrySelectItem()
    {
        //select item
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitData;
        if (Physics.Raycast(ray, out hitData, float.MaxValue, interactableMask))
        {
            if (hitData.transform.TryGetComponent<InteractableItem>(out InteractableItem item))
            {
                selectedItem = item;
                CancelPlaceableItem(); //cancel chosen placeable item when select item
            }
        }
        else
        {
            selectedItem = null;
        }
        OnItemSelected?.Raise(selectedItem);
    }

    //called when item is selected but then want to place item instead
    public void CancelItemSelection()
    {
        selectedItem = null;
        OnItemSelected?.Raise(selectedItem);
    }

    public bool TryPlaceItemAtGrid()
    {
        if (itemToPlaceData == null) return false;

        //ray cast to get a position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitData;
        if (Physics.Raycast(ray, out hitData, float.MaxValue, gridMask))
        {
            GridPosition gridPos = playGrid.GetGridPosition(hitData.point);
            GridItem gridItem = playGrid.GetGridItem(gridPos);
            PathNode pathNodeItem = pathFindingGrid.GetNode(gridPos);

            if (!gridItem.IsPlaceable()) return false;

            InteractableItem item = Instantiate
                (itemToPlaceData.prefab, 
                playGrid.GetWorldPosition(gridPos), Quaternion.identity);
                
            gridItem.SetItem(item); //update placeable at grid position
            pathNodeItem.SetItem(item); //update obstacles for pathfinding

            item.SetGridData(playGrid);
        }

        SetInventoryItem(null);
        OnItemPlaced?.Invoke();
        return true;
    }

    //set item to place into grid
    public void SetInventoryItem(InteractableData activeItemData)
    {
        itemToPlaceData = activeItemData;
    }

    private void CancelPlaceableItem()
    {
        itemToPlaceData = null;
        OnCancelPlacedItem?.Invoke();
    }

    private bool InteractWithUI()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return true;
        }
        return false;
    }

}
