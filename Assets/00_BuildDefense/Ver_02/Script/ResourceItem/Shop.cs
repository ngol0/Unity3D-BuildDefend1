using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    ResourceItem selectedItem;
    public System.Action<ResourceItem> OnSelectedResourceItem;

    public void SetSelectedItem(IInteractable item)
    {
        if (item is ResourceItem)
        {
            selectedItem = item as ResourceItem;
        }
        else
        {
            selectedItem = null;
        }
        OnSelectedResourceItem?.Invoke(selectedItem);
    }

    public void OnTransaction(InteractableData itemData)
    {
        //playPanel.InitNewItem(itemData);
    }
}
