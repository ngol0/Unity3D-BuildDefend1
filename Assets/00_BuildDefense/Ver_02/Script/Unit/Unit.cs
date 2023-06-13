using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : InteractableObject
{
    private PlayGrid playGrid;
    public PlayGrid PlayGrid => playGrid;

    private GridPosition curGridPos;
    public GridPosition CurGridPos => curGridPos;

    BaseAction[] actionArrays;

    private void Start()
    {
        actionArrays = GetComponents<BaseAction>();
    }

    private void Update()
    {
        GridPosition newGridPos = PlayGrid.GetGridPosition(transform.position);

        if (newGridPos != curGridPos)
        {
            PlayGrid.ItemMoveGridPosition(this, curGridPos, newGridPos);
            curGridPos = newGridPos;
        }
    }

    public override void SetGridData(PlayGrid gridSystem)
    {
        this.playGrid = gridSystem;
        curGridPos = PlayGrid.GetGridPosition(transform.position);
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
