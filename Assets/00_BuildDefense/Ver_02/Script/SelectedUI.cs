using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedUI : MonoBehaviour
{   
    [SerializeField] Interactable item;
     private void Start() 
    {
        gameObject.SetActive(false);
    }

    public void SetSelectionUI(Interactable selectedItem)
    {
        if (item == selectedItem) gameObject.SetActive(true);
        if (item == null || item != selectedItem) gameObject.SetActive(false);
    }
}
