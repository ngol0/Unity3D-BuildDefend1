using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedUI : MonoBehaviour
{   
    [SerializeField] IInteractable item;
    [SerializeField] MeshRenderer mesh;
     private void Start() 
    {
        mesh.enabled = false;
    }

    public void SetSelectionUI(IInteractable selectedItem)
    {
        if (item == selectedItem) mesh.enabled = true;
        if (item == null || item != selectedItem) mesh.enabled = false;
    }
}
