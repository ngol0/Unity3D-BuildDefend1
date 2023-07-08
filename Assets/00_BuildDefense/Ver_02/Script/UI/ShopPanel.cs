using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    ResourceItem selectedItem;
    public ResourceItem SelectedItem => selectedItem;
    [SerializeField] PlaceableItemPanel placeablePanel;

    public System.Action OnGetSelectedItem;

    public void SetSelectedItem(IInteractable item)
    {
        if (item != null && !item.IsMoveable)
        {
            selectedItem = item as ResourceItem;
        }
        else
        {
            selectedItem = null;
        }
        OnGetSelectedItem?.Invoke();
    }

    public void OnTransaction(InteractableData itemData)
    {
        Debug.Log("EEEh");
        placeablePanel.InitNewItem(itemData);
    }
}
