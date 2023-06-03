using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    private Grid<PathNode> gridSystem;
    private GridPosition gridPosition;
    private int fCost;
    private int gCost;
    private int hCost;

    public int FCost => fCost;
    public int GCost => gCost;
    public int HCost => hCost;
    
    private PathNode cameFromNode;

    public PathNode(Grid<PathNode> gridSystem, GridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
    }

    public override string ToString()
    {
        return gridPosition.ToString();
    }
}
