using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/InventorySO")]
public class InventorySO : ScriptableObject
{
    public List<InteractableData> interactableItemList;
    public Action<InteractableData> OnAddComplete;

    public void AddToInteractableList(InteractableData item)
    {
        interactableItemList.Add(item);
        OnAddComplete?.Invoke(item);
    }

    public void RemoveInteractableItem(InteractableData item)
    {
        interactableItemList.Remove(item);
        Debug.Log(":::Inventory Remove: " + item.name);
    }
}
