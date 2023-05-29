using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour
{
    [SerializeField] LayerMask interactableMask;

    Interactable selectedItem;
    
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
            if (hitData.transform.TryGetComponent<Interactable>(out Interactable item))
            {
                selectedItem = item;
            }
        }
    }

    private void UpdateSelectionUI()
    {

    }
}
