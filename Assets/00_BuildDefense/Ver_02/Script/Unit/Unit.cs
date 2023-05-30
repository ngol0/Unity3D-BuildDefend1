using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : InteractableObject
{
    GridPosition curGridPos;
    GridPosition nextGridPos;
    GridManager gridManager;
    Vector3 targetPos;
    Vector3 moveDirection = new Vector3(1,0,0);
    float moveSpeed = 5f;
    int curRow;

    private void Awake() 
    {
        targetPos = transform.position;
    }

    private void Update() 
    {
        //Debug.Log(Vector3.Distance(transform.position, targetPos));
        if (Vector3.Distance(transform.position, targetPos) > 0.1f)
        {
            transform.position += moveDirection * Time.deltaTime * moveSpeed;

            GridPosition newGridPos = gridManager.GetGridPosition(transform.position);

            if (newGridPos != curGridPos)
            {
                gridManager.ItemMoveGridPosition(this, curGridPos, newGridPos);
                curGridPos = newGridPos;
            }
        }
    }

    public void Move()
    {
        targetPos = gridManager.GetWorldPosition(gridManager.GetLastPosInRow(curRow));

        //todo: set path
        
    }

    public void Stop()
    {
        if (transform.position.x < gridManager.GetWorldPosition(curGridPos).x)
        {
            targetPos = gridManager.GetWorldPosition(curGridPos);
        }
        else
        {
            nextGridPos = new GridPosition(curGridPos.x + 1, curGridPos.z);
            targetPos = gridManager.GetWorldPosition(nextGridPos);
        }
    }

    public override void SetGridSystem(GridManager gridSystem)
    {
        this.gridManager = gridSystem;
        curGridPos = gridSystem.GetGridPosition(transform.position);
        curRow = curGridPos.z;
    }

    public override bool IsMoveable => true;
}
