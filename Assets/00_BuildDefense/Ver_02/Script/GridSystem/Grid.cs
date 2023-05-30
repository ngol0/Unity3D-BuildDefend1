using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private int gridHeight;
    private int gridWidth;
    private int cellSize;
    private GridItem[,] gridItemArray;

    public Grid(int gridWidth, int gridHeight, int cellSize)
    {
        this.gridWidth = gridWidth;
        this.gridHeight = gridHeight;
        this.cellSize = cellSize;
    }

    public void CreateGrid(GridItemUI gridItemPrefab, Transform root)
    {
        gridItemArray = new GridItem[gridWidth, gridHeight];
        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridHeight; z++)
            {
                GridPosition gridPos = new GridPosition(x,z);

                //create grid array data
                gridItemArray[x,z] = new GridItem(this, gridPos);

                //create grid ui
                GridItemUI gridItemUI = 
                    GameObject.Instantiate<GridItemUI>(gridItemPrefab, GetWorldPosition(gridPos), Quaternion.identity, root);
                gridItemUI.SetGridItem(GetGridItem(gridPos));
            }
        }
    }

    public GridItem GetGridItem(GridPosition gridPos)
    {
        return gridItemArray[gridPos.x, gridPos.z];
    }

    public Vector3 GetWorldPosition(GridPosition gridPos)
    {
        return new Vector3(gridPos.x, 0, gridPos.z) * cellSize;
    }

    public GridPosition GetGridPosition(Vector3 worldPos)
    {
        return new GridPosition(
            Mathf.RoundToInt(worldPos.x/cellSize), 
            Mathf.RoundToInt(worldPos.z/cellSize)
        );
    }

    public bool IsValidGridPos(GridPosition gridPos)
    {
        return gridPos.x < gridWidth && gridPos.z < gridHeight;
    }

    public GridPosition GetLastGridPosInRow(int row)
    {
        return new GridPosition(gridWidth-1, row);
    }
}
