using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedUI : MonoBehaviour
{
    [SerializeField] Interactable item;
    private void Start() 
    {
        TurnOffSelection();
    }
    public void HandleSelection()
    {
        gameObject.SetActive(true);
    }

    public void TurnOffSelection()
    {
        gameObject.SetActive(false);
    }
}
