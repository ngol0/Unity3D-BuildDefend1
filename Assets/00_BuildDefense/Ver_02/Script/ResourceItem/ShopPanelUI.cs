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
    public void SetUI(ResourceItem item, ResourceController resourceController)
    {
        if (item!=null)
        {
            InitItems(item, resourceController);
            guiMain.SetActive(true);
        }
        else
        {
            guiMain.SetActive(false);
        }
    }

    //init items to sell in the selected item
    public void InitItems(ResourceItem resourceItem, ResourceController resourceController)
    {
        foreach (Transform item in rootSpawn)
        {
            Destroy(item.gameObject);
        }

        foreach (ResourceItemData item in resourceItem.Data.itemToSell)
        {
            var btn = Instantiate<ShopItemUI>(buttonPrefab, rootSpawn);
            btn.SetData(item, shop);
            btn.SetBtnActive(resourceController.CanAfford(item.resourceCostToBuild));
            btn.gameObject.SetActive(true);
        }
    }
}
