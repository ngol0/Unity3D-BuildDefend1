using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Grid Stats")]
    [SerializeField] int gridWidth = 10;
    [SerializeField] int gridHeight = 5;
    [SerializeField] int cellSize = 20;
    [SerializeField] LayerMask gridMask;

    [Header("UI")]
    [SerializeField] GridItemUI gridItemPrefab;

    [Header("Placeable items")]
    [SerializeField] Interactable activeItem;

    Grid gridSystem;

    private void Awake() 
    {
        gridSystem = new Grid(gridWidth,gridHeight,cellSize);
        gridSystem.CreateGrid(gridItemPrefab, transform);
    }

    private void Update() 
    {
        if (Input.GetMouseButtonDown(0)) { TryPlaceItemAtGrid(); }
    }

    public void SetActiveItem(Interactable activeItem)
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
            GridPosition gridPos = gridSystem.WorldToGridPos(hitData.transform.position);
            GridItem gridItem = GetItemAtGrid(gridPos);

            if(gridItem.IsPlaceable())
            {
                Interactable item = Instantiate(activeItem, gridSystem.GridToWorldPos(gridPos), Quaternion.identity);
                gridItem.SetItem(item);
            }
        }

        SetActiveItem(null);
    }

    public void RemoveItemAtGrid(GridPosition gridPosition)
    {
        GridItem gridItem = GetItemAtGrid(gridPosition);
        gridItem.SetItem(null);
    }

    public void SetItemAtGrid(Interactable item, GridPosition gridPosition)
    {
        GridItem gridItem = GetItemAtGrid(gridPosition);
        gridItem.SetItem(item);
    }

    public void ItemMoveGridPosition(Interactable item, GridPosition fromGridPos, GridPosition toGridPos)
    {
        RemoveItemAtGrid(fromGridPos);
        SetItemAtGrid(item, toGridPos);
    }

    public GridItem GetItemAtGrid(GridPosition gridPosition)
    {
        return gridSystem.GetGridItem(gridPosition);
    }
}
