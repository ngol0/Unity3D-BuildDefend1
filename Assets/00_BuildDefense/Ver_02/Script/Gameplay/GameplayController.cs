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

    #region Events
    public System.Action OnItemPlaced;
    public System.Action OnCancelPlacedItem;
    public System.Action OnTryPlacingResourceItem;
    public System.Action OnDoneDeciding;
    #endregion

    InteractableItem activePlayableItem; //selected existing item
    InteractableItem activePlaceableItem; //item that is waiting to be confirmed to place
    InteractableData itemToPlaceInfo; //data of item to place

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
        if (Physics.Raycast(ray, out RaycastHit hitData, float.MaxValue, interactableMask))
        {
            if (hitData.transform.TryGetComponent<InteractableItem>(out InteractableItem item))
            {
                activePlayableItem = item;
                CancelPlaceableItem(); //cancel chosen placeable item when select item
            }
        }
        else
        {
            activePlayableItem = null;
        }

        if (OnItemSelected) OnItemSelected.Raise(activePlayableItem);

    }

    //called when item is selected but then want to place item instead
    public void CancelItemSelection()
    {
        activePlayableItem = null;
        if (OnItemSelected) OnItemSelected.Raise(activePlayableItem);
    }

    public bool TryPlaceItemAtGrid()
    {
        if (itemToPlaceInfo == null) return false;

        //ray cast to get a position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitData, float.MaxValue, gridMask))
        {
            GridPosition gridPos = playGrid.GetGridPosition(hitData.point);
            GridItem gridItem = playGrid.GetGridItem(gridPos);

            if (!gridItem.IsPlaceable()) return false;

            InteractableItem item = Instantiate
                (itemToPlaceInfo.prefab,
                playGrid.GetWorldPosition(gridPos), Quaternion.identity);

            item.SetGridData(playGrid, pathFindingGrid);
            CheckItemOnPlace(item);
        }
        return true;
    }

    private void CheckItemOnPlace(InteractableItem item)
    {
        activePlaceableItem = item;

        if (activePlaceableItem is ResourceItem) //if item resource item (i.e: houses)
        {
            activePlaceableItem.GetComponent<ResourceGeneratorRepresentation>().FindResourceNodeNearby(); //find resource to show ++ sign
            OnTryPlacingResourceItem?.Invoke(); //update ui: show yes or no panel
        }
        else if (activePlaceableItem is Unit)
        {
            DonePlacingItem();
        }
    }

    //bind to button at Confirmation Panel
    private void OnDecideToPlace(bool isPlaced)
    {
        if (isPlaced)
        {
            activePlaceableItem.GetComponent<ResourceGenerator>().SetMaxTimer(); //set timer for resource item to start collecting resource
            DonePlacingItem(); //remove from inventory
        }
        else
        {
            Destroy(activePlaceableItem.gameObject);
            CancelPlaceableItem();
        }

        OnDoneDeciding?.Invoke(); //show inventory ui
    }

    public void DonePlacingItem()
    {
        //update placeable at grid position && update obstacles for pathfinding
        playGrid.SetItemAtGrid(activePlaceableItem, activePlaceableItem.CurGridPos);
        pathFindingGrid.SetItemAtGrid(activePlaceableItem, activePlaceableItem.CurGridPos);

        inventory.RemoveInteractableItem(itemToPlaceInfo);
        OnItemPlaced?.Invoke(); //update inventory ui

        SetItemToPlaceInfo(null);
        activePlaceableItem = null;
    }

    //set item to place into grid
    public void SetItemToPlaceInfo(InteractableData activeItemData)
    {
        itemToPlaceInfo = activeItemData;
    }

    private void CancelPlaceableItem()
    {
        itemToPlaceInfo = null;
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
