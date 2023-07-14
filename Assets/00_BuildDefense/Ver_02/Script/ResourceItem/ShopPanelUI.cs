using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanelUI : MonoBehaviour
{
    [Header("UI Ref")]
    [SerializeField] GameObject guiMain;
    [SerializeField] ShopItemUI buttonPrefab;
    [SerializeField] Transform rootSpawn;

    [Header("Shop")]
    [SerializeField] Shop shop;

    private void Start() 
    {
        shop.OnSelectedResourceItem += SetUI;
    }
    public void SetUI(ResourceItem item)
    {
        if (item!=null)
        {
            guiMain.SetActive(true);
            InitItems(item);
        }
        else
        {
            guiMain.SetActive(false);
        }
    }

    //init items to sell in the selected item
    public void InitItems(ResourceItem resourceItem)
    {
        foreach (Transform item in rootSpawn)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in resourceItem.Data.itemToSell)
        {
            var btn = Instantiate<ShopItemUI>(buttonPrefab, rootSpawn);
            btn.SetData(item, shop);
            btn.gameObject.SetActive(true);
        }
    }
}
