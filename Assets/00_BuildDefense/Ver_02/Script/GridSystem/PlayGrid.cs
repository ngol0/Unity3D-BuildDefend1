using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGrid : MonoBehaviour
{
    [Header("Grid Stats")]
    [SerializeField] int gridWidth = 10;
    [SerializeField] int gridHeight = 6;
    [SerializeField] int cellSize = 20;
    [SerializeField] LayerMask gridMask;
    [SerializeField] LayerMask obstacleMask;

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
        gridSystem = new GridSystem<GridItem>(gridWidth, gridHeight, cellSize,
            (GridSystem<GridItem> g, GridPosition gridPos) => new GridItem(g, gridPos)
        );

        gridSystem.CreateGridUI(gridItemPrefab, transform);

        SetUpPlaceable();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            TryPlaceItemAtGrid();
    }

    public void SetActiveItem(InteractableObject activeItem)
    {
        this.activeItem = activeItem;
    }

    private void SetUpPlaceable()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridHeight; z++)
            {
                GridPosition gridPos = new GridPosition(x, z);
                RaycastHit hitData;
                if (Physics.Raycast(GetWorldPosition(gridPos) + Vector3.down * 5f, Vector3.up, out hitData, float.MaxValue, obstacleMask))
                {
                    if (hitData.transform.TryGetComponent<IObject>(out IObject item))
                    {
                        gridSystem.GetGridItem(gridPos).SetItem(item);
                        Debug.Log("eh?");
                    }
                }
            }
        }
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

            if (gridItem.IsPlaceable())
            {
                InteractableObject item = Instantiate(activeItem, gridSystem.GetWorldPosition(gridPos), Quaternion.identity);
                gridItem.SetItem(item);
                item.SetGridData(this);
            }
        }

        SetActiveItem(null);
    }

    public void RemoveItemAtGrid(GridPosition gridPosition)
    {
        GridItem gridItem = GetGridItem(gridPosition);
        gridItem.SetItem(null);
    }

    public void SetItemAtGrid(InteractableObject item, GridPosition gridPosition)
    {
        GridItem gridItem = GetGridItem(gridPosition);
        gridItem.SetItem(item);
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
