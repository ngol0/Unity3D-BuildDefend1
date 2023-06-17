using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseInputController : MonoBehaviour
{
    [Header("Layer Mask for Raycast")]
    [SerializeField] LayerMask interactableMask;
    [SerializeField] LayerMask gridMask;

    [Header("Ref")]
    [SerializeField] PlayGrid gridSystem;

    [Header("Event to raise")]
    [SerializeField] InteractableEvent OnItemSelected;

    IInteractable selectedItem;
    [SerializeField] IInteractable readyToPlaceItem;

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
        if (readyToPlaceItem == null) return false;

        //ray cast to get a position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitData;
        if (Physics.Raycast(ray, out hitData, float.MaxValue, gridMask))
        {
            GridPosition gridPos = gridSystem.GetGridPosition(hitData.point);
            GridItem gridItem = gridSystem.GetGridItem(gridPos);

            if (!gridItem.IsPlaceable()) return false;

            IInteractable item = Instantiate(readyToPlaceItem, gridSystem.GetWorldPosition(gridPos), Quaternion.identity);
            gridItem.SetItem(item);
            item.SetGridData(gridSystem);
        }

        SetActiveItem(null);
        return true;
    }

    public void SetActiveItem(IInteractable activeItem)
    {
        readyToPlaceItem = activeItem;
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
