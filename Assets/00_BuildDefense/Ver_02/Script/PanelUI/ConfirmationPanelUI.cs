using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmationPanelUI : MonoBehaviour
{
    [SerializeField] GameplayController controller;
    [SerializeField] GameObject canvas;

    private void OnEnable() 
    {
        controller.OnTryPlacingResourceItem += SetPanelActive;
        controller.OnDoneDeciding += DeactivateUI;
    }

    public void SetPanelActive()
    {
        canvas.SetActive(true);
    }

    public void DeactivateUI()
    {
        canvas.SetActive(false);
    }
}
