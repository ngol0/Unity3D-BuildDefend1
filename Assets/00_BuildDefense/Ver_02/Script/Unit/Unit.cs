using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : InteractableItem
{
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

    public T GetAction<T>() where T : BaseAction
    {
        foreach (BaseAction action in actionArrays)
        {
            if (action is T t)
            {
                return t;
            }
        }

        return null;
    }

    public override void SetGridData(PlayGrid gridSystem)
    {
        base.SetGridData(gridSystem);
        Debug.Log(":::Unit placed");
    }

    //----testing----//
    // public override void SetGridData(PlayGrid gridSystem)
    // {
    //     base.SetGridData(gridSystem);
    //     TestingSetup();
    // }
    // void TestingSetup()
    // {
    //     testing = FindObjectOfType<Testing>();
    //     testing.SetStartingPoint(curGridPos);
    //     testing.Move += TestMove;
    // }

    // void TestMove()
    // {
    //     GetAction<MoveAction>().SetPath(testing.list);
    // }

    //---end testing---//
}
