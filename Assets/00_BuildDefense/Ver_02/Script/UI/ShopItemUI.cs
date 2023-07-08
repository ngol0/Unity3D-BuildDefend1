using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    InteractableData data;
    ShopPanel shop;

    [SerializeField] Image spriteImg;
    public void SetData(InteractableData data, ShopPanel shop)
    {
        this.data = data;
        this.shop = shop;
        SetUI();
    }

    private void SetUI()
    {
        spriteImg.sprite = data.sprite;
    }

    public void OnShopItemClicked()
    {
        shop.OnTransaction(data);
    }
}
