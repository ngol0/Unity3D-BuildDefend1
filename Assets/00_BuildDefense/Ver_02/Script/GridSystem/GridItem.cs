using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridItem
{
    private GridSystem<GridItem> gridSystem;
    private GridPosition gridPosition;
    private IGameItem item;

    public GridItem(GridSystem<GridItem> gridSystem, GridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
    }

    public void SetItem(IGameItem item)
    {
        this.item = item;
    }

    public bool IsPlaceable()
    {
        return item==null;
    }

    public override string ToString()
    {
        if (item!=null) return item.GetName();
        else return gridPosition.ToString();
    }
}
