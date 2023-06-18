using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMoveActionUI : MonoBehaviour
{
    [SerializeField] GameObject guiMain;
    public void SetUnitControllerActive(IInteractable item)
    {
        if (item!=null && item.IsMoveable)
        {
            guiMain.SetActive(true);
            return;
        }
        guiMain.SetActive(false);
    }
}
