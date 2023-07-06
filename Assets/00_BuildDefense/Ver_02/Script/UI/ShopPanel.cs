using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    [SerializeField] GameObject guiMain;
    [SerializeField] ShopItemUI buttonPrefab;
    [SerializeField] Transform rootSpawn;
    ResourceItem selectedItem;

    public void SetPanelActive(IInteractable item)
    {
        if (item!=null && !item.IsMoveable)
        {
            selectedItem = item as ResourceItem;
            Debug.Log(selectedItem.name);
            guiMain.SetActive(true);
            InitItems();
            return;
        }
        guiMain.SetActive(false);
    }

    public void InitItems()
    {
        foreach (Transform item in rootSpawn)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in selectedItem.Data.unitToSell)
        {
            Debug.Log(item.name);
            var btn = Instantiate<ShopItemUI>(buttonPrefab, rootSpawn);
            btn.SetData(item);
            btn.gameObject.SetActive(true);
        }
    }
}
