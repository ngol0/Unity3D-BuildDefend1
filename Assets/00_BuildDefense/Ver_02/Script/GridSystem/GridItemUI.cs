using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridItemUI : MonoBehaviour
{
    object gridItem;
    [SerializeField] TextMesh text;
    
    public void SetGridItem(object gridItem)
    {
        this.gridItem = gridItem;
    }

    private void Update() 
    {
        text.text = gridItem.ToString();
    }
}
