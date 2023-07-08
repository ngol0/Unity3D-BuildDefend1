using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanelUI : MonoBehaviour
{
    [SerializeField] GameObject guiMain;
    [SerializeField] ShopItemUI buttonPrefab;
    [SerializeField] Transform rootSpawn;
    [SerializeField] ShopPanel shop;

    private void Start() 
    {
        shop.OnGetSelectedItem += SetPanelActive;
    }

    public void SetPanelActive()
    {
        if (shop.SelectedItem == null) 
        {
            guiMain.SetActive(false);
        }
        else
        {
            guiMain.SetActive(true);
            InitItems();
        }
    }

    public void InitItems()
    {
        foreach (Transform item in rootSpawn)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in shop.SelectedItem.Data.unitToSell)
        {
            var btn = Instantiate<ShopItemUI>(buttonPrefab, rootSpawn);
            btn.SetData(item, shop);
            btn.gameObject.SetActive(true);
        }
    }
}
