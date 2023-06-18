using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyableItemPanel : MonoBehaviour
{
    [SerializeField] GameObject guiMain;
    public void SetPanelActive(IInteractable item)
    {
        if (item!=null && !item.IsMoveable)
        {
            guiMain.SetActive(true);
            return;
        }
        guiMain.SetActive(false);
    }
}
