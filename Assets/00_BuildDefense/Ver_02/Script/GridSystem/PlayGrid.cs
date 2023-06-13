using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGrid : MonoBehaviour
{
    [Header("Grid Stats")]
    [SerializeField] int gridWidth = 10;
    [SerializeField] int gridHeight = 5;
    [SerializeField] int cellSize = 20;
    [SerializeField] LayerMask gridMask;

    [Header("UI")]
    [SerializeField] GridItemUI gridItemPrefab;

    [Header("Placeable items")]
    [SerializeField] InteractableObject activeItem;

    GridSystem<GridItem> gridSystem;

    public int GridWidth => gridWidth;
    public int GridHeight => gridHeight;
    public int CellSize => cellSize;


    private void Awake() 
    {
        //delegate using lambda
        gridSystem = new GridSystem<GridItem>(gridWidth,gridHeight,cellSize, 
            (GridSystem<GridItem> g, GridPosition gridPos) => new GridItem(g, gridPos)
        ); 
    }

    private void Update() 
    {
        // if (Input.GetMouseButtonDown(0)) 
        // { TryPlaceItemAtGrid(); }
    }

    public void SetActiveItem(InteractableObject activeItem)
    {
        this.activeItem = activeItem;
    }

    public void TryPlaceItemAtGrid()
    {
        if (activeItem == null) return;

        //ray cast to get a position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitData;
        if (Physics.Raycast(ray, out hitData, float.MaxValue, gridMask))
        {
            GridPosition gridPos = gridSystem.GetGridPosition(hitData.point);
            GridItem gridItem = GetGridItem(gridPos);

            if(gridItem.IsPlaceable())
            {
                InteractableObject item = Instantiate(activeItem, gridSystem.GetWorldPosition(gridPos), Quaternion.identity);
                gridItem.SetInteractableItem(item);
                item.SetGridData(this);
            }
        }

        SetActiveItem(null);
    }

    public void RemoveItemAtGrid(GridPosition gridPosition)
    {
        GridItem gridItem = GetGridItem(gridPosition);
        gridItem.SetInteractableItem(null);
    }

    public void SetItemAtGrid(InteractableObject item, GridPosition gridPosition)
    {
        GridItem gridItem = GetGridItem(gridPosition);
        gridItem.SetInteractableItem(item);
    }

    public void ItemMoveGridPosition(InteractableObject item, GridPosition fromGridPos, GridPosition toGridPos)
    {
        RemoveItemAtGrid(fromGridPos);
        SetItemAtGrid(item, toGridPos);
    }

    public GridItem GetGridItem(GridPosition gridPosition) => gridSystem.GetGridItem(gridPosition);
    public GridPosition GetGridPosition(Vector3 worldPos) => gridSystem.GetGridPosition(worldPos);
    public Vector3 GetWorldPosition(GridPosition gridPos) => gridSystem.GetWorldPosition(gridPos);
    public GridPosition GetLastGridInRow(int row) => gridSystem.GetLastGridPosInRow(row);
}
