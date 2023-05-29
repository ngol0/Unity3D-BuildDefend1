using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : InteractableObject
{
    bool isMoving = false;
    GridPosition curGridPos;
    GridManager gridSystem;

    private void Start() 
    {
        
    }

    private void Update() 
    {
        if (isMoving)
        {
            transform.position = 
                transform.position + new Vector3(1,0,0) * Time.deltaTime * 10;

            GridPosition newGridPos = gridSystem.GetGridPosition(transform.position);

            if (newGridPos != curGridPos)
            {
                gridSystem.ItemMoveGridPosition(this, curGridPos, newGridPos);
                curGridPos = newGridPos;
            }
        }
    }

    public void Move()
    {
        isMoving = true;
    }

    public void Stop()
    {
        isMoving = false;
    }

    public override void SetGridSystem(GridManager grid, GridPosition gridPosition)
    {
        gridSystem = grid;
        curGridPos = gridPosition;
    }

    public override bool IsMoveable => true;
}
