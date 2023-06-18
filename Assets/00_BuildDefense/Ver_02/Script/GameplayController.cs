using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameplayController : MonoBehaviour
{
    [Header("Layer Mask for Raycast")]
    [SerializeField] LayerMask interactableMask;
    [SerializeField] LayerMask gridMask;

    [Header("Ref")]
    [SerializeField] PlayGrid playGrid;
    [SerializeField] Pathfinding pathFindingGrid;

    [Header("Event to raise")]
    [SerializeField] InteractableEvent OnItemSelected;
    public System.Action OnItemPlaced;


    IInteractable selectedItem;
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
            selectedItem = (hitData.transform.TryGetComponent<IInteractable>(out IInteractable item)) ? item : null;
        }
        else
        {
            selectedItem = null;
        }
        OnItemSelected?.Raise(selectedItem);
    }

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

            IInteractable item = Instantiate
                (itemToPlaceData.housePrefab, 
                playGrid.GetWorldPosition(gridPos), Quaternion.identity);
                
            gridItem.SetItem(item); //update placeable at grid position
            pathNodeItem.SetItem(item);

            item.SetGridData(playGrid);
        }

        SetActiveItemData(null);
        OnItemPlaced?.Invoke();
        return true;
    }

    public void SetActiveItemData(InteractableData activeItemData)
    {
        itemToPlaceData = activeItemData;
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
