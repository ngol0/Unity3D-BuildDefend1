using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridItemUI : MonoBehaviour
{
    GridItem gridItem;
    [SerializeField] TextMesh text;
    
    public void SetGridItem(GridItem gridItem)
    {
        this.gridItem = gridItem;
    }

    private void Update() 
    {
        text.text = gridItem.ToString();
    }
}
