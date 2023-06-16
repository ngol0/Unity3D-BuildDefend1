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

    //----TESTING---//
    public Testing testing;

    private void Start()
    {
        actionArrays = GetComponents<BaseAction>();
    }

    private void Update()
    {
        GridPosition newGridPos = playGrid.GetGridPosition(transform.position);

        if (newGridPos != curGridPos)
        {
            playGrid.ItemMoveGridPosition(this, curGridPos, newGridPos);
            curGridPos = newGridPos;
            
            //testing.SetStartingPoint(curGridPos);
        }
    }

    public override void SetGridData(PlayGrid gridSystem)
    {
        this.playGrid = gridSystem;
        curGridPos = playGrid.GetGridPosition(transform.position);

    //----testing----//
        // testing = FindObjectOfType<Testing>();
        // testing.SetStartingPoint(curGridPos);
        // testing.Move += TestMove;
    }

    // void TestMove()
    // {
    //     GetAction<MoveAction>().SetPath(testing.list);
    // }

    //---end testing---//

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
