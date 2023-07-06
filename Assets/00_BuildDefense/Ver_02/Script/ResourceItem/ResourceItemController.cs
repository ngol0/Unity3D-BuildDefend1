using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceItemController : MonoBehaviour
{
    ResourceItem selectedItem;
    public System.Action<ResourceItem> OnInitShop;
    public void SetSelectedItem(IInteractable item)
    {
        if (item != null && !item.IsMoveable)
        {
            selectedItem = item as ResourceItem;
            return;
        }
        selectedItem = null;
        OnInitShop?.Invoke(selectedItem);
    }
}
