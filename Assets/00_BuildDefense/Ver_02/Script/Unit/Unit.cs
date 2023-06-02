using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : InteractableObject
{
    private GridManager gridManager;
    public GridManager GridManager => gridManager;

    private GridPosition curGridPos;
    public GridPosition CurGridPos => curGridPos;

    BaseAction[] actionArrays;

    private void Start()
    {
        actionArrays = GetComponents<BaseAction>();
    }

    private void Update()
    {
        GridPosition newGridPos = GridManager.GetGridPosition(transform.position);

        if (newGridPos != curGridPos)
        {
            GridManager.ItemMoveGridPosition(this, curGridPos, newGridPos);
            curGridPos = newGridPos;
        }
    }

    public override void SetGridData(GridManager gridSystem)
    {
        this.gridManager = gridSystem;
        curGridPos = GridManager.GetGridPosition(transform.position);
    }

    public override bool IsMoveable => true;

    public T GetAction<T>() where T : BaseAction
    {
        foreach (BaseAction action in actionArrays)
        {
            if (action is T)
            {
                return (T)action;
            }
        }

        return null;
    }
}
