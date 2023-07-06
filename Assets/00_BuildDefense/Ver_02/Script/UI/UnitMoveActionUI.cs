using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMoveActionUI : MonoBehaviour
{
    [SerializeField] GameObject guiMain;
    [SerializeField] UnitActionController logicController;

    private void Start() 
    {
        logicController.OnSelectedUnit += SetUnitControllerActive;
    }

    public void SetUnitControllerActive(bool active)
    {
        guiMain.SetActive(active);
    }
}
