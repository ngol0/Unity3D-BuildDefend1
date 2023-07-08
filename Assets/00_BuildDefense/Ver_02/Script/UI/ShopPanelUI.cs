using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanelUI : MonoBehaviour
{
    [Header("UI Ref")]
    [SerializeField] GameObject guiMain;
    [SerializeField] ShopItemUI buttonPrefab;
    [SerializeField] Transform rootSpawn;

    [Header("Play Panel")]
    [SerializeField] PlayPanelUI playPanel;

    ResourceItem selectedItem;
    public ResourceItem SelectedItem => selectedItem;

    public void SetSelectedItem(IInteractable item)
    {
        if (item != null && !item.IsMoveable)
        {
            selectedItem = item as ResourceItem;
            guiMain.SetActive(true);
            InitItems();
            return;
        }
        guiMain.SetActive(false);
    }

    //init items to sell in the selected item
    public void InitItems()
    {
        foreach (Transform item in rootSpawn)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in selectedItem.Data.unitToSell)
        {
            var btn = Instantiate<ShopItemUI>(buttonPrefab, rootSpawn);
            btn.SetData(item, this);
            btn.gameObject.SetActive(true);
        }
    }

    public void OnTransaction(InteractableData itemData)
    {
        playPanel.InitNewItem(itemData);
    }
}
