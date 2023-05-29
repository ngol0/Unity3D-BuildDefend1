using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridItem
{
    private Grid gridSystem;
    private GridPosition gridPosition;
    IPlaceable item;

    public GridItem(Grid gridSystem, GridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
    }

    public void SetItem(IPlaceable item)
    {
        this.item = item;
    }

    public bool IsPlaceable()
    {
        return item==null;
    }
}
