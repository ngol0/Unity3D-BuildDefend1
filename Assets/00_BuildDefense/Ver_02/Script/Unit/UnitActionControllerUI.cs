using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionControllerUI : MonoBehaviour
{
    [SerializeField] GameObject guiMain;
    public void SetUnitControllerActive(InteractableObject item)
    {
        if (item!=null && item.IsMoveable)
        {
            guiMain.SetActive(true);
            return;
        }
        guiMain.SetActive(false);
    }
}
