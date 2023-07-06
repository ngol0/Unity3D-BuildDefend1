using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    InteractableData data;

    [SerializeField] Image spriteImg;
    public void SetData(InteractableData data)
    {
        this.data = data;
        SetUI();
    }

    private void SetUI()
    {
        spriteImg.sprite = data.sprite;
    }
}
