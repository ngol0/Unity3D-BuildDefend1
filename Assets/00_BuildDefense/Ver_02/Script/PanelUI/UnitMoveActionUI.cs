using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMoveActionUI : MonoBehaviour
{
    [Header("UI Ref")]
    [SerializeField] GameObject guiMain;
    [SerializeField] InventoryUI playPanel;

    [Header("Logic")]
    [SerializeField] UnitActionController logicController;

    private void Start() 
    {
        logicController.OnSelectedUnit += SetUnitControllerActive;
    }

    public void SetUnitControllerActive(bool active)
    {
        playPanel.gameObject.SetActive(!active);
        guiMain.SetActive(active);
    }
}
