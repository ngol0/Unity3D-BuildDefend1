using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedUI : MonoBehaviour
{   
    [SerializeField] InteractableObject item;
     private void Start() 
    {
        gameObject.SetActive(false);
    }

    public void SetSelectionUI(InteractableObject selectedItem)
    {
        if (item == selectedItem) gameObject.SetActive(true);
        if (item == null || item != selectedItem) gameObject.SetActive(false);
    }
}
