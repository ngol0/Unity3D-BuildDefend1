using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridItem
{
    private Grid gridSystem;
    private GridPosition gridPosition;
    InteractableObject item;

    public GridItem(Grid gridSystem, GridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
    }

    public void SetInteractableItem(InteractableObject item)
    {
        this.item = item;
    }

    public bool IsPlaceable()
    {
        return item==null;
    }

    public override string ToString()
    {
        if (item!=null) return "Unit";
        else return "(" + gridPosition.x + ", " + gridPosition.z + ")";
    }
}
