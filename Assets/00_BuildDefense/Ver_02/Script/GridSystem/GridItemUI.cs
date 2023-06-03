using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridItemUI : MonoBehaviour
{
    object gridItem;
    [SerializeField] TextMesh coordinateText;
    
    public virtual void SetGridItem(object gridItem)
    {
        this.gridItem = gridItem;
        //Debug.Log(gridItem.GetType());
    }

    protected virtual void Update() 
    {
        coordinateText.text = gridItem.ToString();
    }
}
