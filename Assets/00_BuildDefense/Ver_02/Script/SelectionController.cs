using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionController : MonoBehaviour
{
    [SerializeField] LayerMask interactableMask;
    [SerializeField] InteractableEvent OnItemSelected;

    InteractableObject selectedItem;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
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
            if (hitData.transform.TryGetComponent<InteractableObject>(out InteractableObject item))
            {
                selectedItem = item;
            }
        }

        OnItemSelected?.Raise(selectedItem);
    }

    public void CancelItemSelection()
    {
        selectedItem = null;
        OnItemSelected?.Raise(selectedItem);
    }
}
