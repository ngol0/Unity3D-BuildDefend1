using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGrid : GridBase
{
    [Header("UI")]
    [SerializeField] GridItemUI gridItemPrefab;

    GridSystem<GridItem> gridSystem;

    private void Awake()
    {
        //delegate using lambda
        gridSystem = new GridSystem<GridItem>(gridStats.gridWidth, gridStats.gridHeight, gridStats.cellSize,
            (GridSystem<GridItem> g, GridPosition gridPos) => new GridItem(g, gridPos)
        );

        gridSystem.CreateGridUI(gridItemPrefab, transform);
        InitialSetUp();
    }

    public override void SetItemAtGrid(IGameItem item, GridPosition gridPos)
    {
        GetGridItem(gridPos).SetItem(item);
    }
    
    public void RemoveItemAtGrid(GridPosition gridPos)
    {
        GetGridItem(gridPos).SetItem(null);
    }

    public void ItemMoveGridPosition(InteractableItem item, GridPosition fromGridPos, GridPosition toGridPos)
    {
        RemoveItemAtGrid(fromGridPos);
        SetItemAtGrid(item, toGridPos);
    }

    public bool IsGridItemPlaceable(GridPosition gridPos)
    {
        return GetGridItem(gridPos).IsPlaceable();
    }

    public GridItem GetGridItem(GridPosition gridPosition) => gridSystem.GetGridItem(gridPosition);
    public override GridPosition GetGridPosition(Vector3 worldPos) => gridSystem.GetGridPosition(worldPos);
    public override Vector3 GetWorldPosition(GridPosition gridPos) => gridSystem.GetWorldPosition(gridPos);
}
