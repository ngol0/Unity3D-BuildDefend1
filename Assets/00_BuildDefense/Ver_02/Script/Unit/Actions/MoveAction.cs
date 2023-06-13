using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : BaseAction
{
    private Vector3 targetPos;
    private Unit unit;
    private float moveSpeed = 5f;
    private Vector3 moveDirection;
    private GridPosition nextGridPos;
    private PlayGrid playGrid;
    private Queue<GridPosition> gridTargets = new Queue<GridPosition>();

    bool setNextTarget = false;

    private void Awake()
    {
        targetPos = transform.position;
    }

    private void Start()
    {
        unit = GetComponent<Unit>();
        playGrid = unit.PlayGrid;
    }


    private void Update()
    {
        //Debug.Log(Vector3.Distance(transform.position, targetPos));
        DequeueGridTarget();

        if (Vector3.Distance(transform.position, targetPos) > 0.1f)
        {
            moveDirection = (targetPos - transform.position).normalized;
            transform.position += moveDirection * Time.deltaTime * moveSpeed;
        }
        else
        {
            setNextTarget = true;
        }
    }

    void DequeueGridTarget()
    {
        if (gridTargets.Count > 0 && setNextTarget)
        {
            var gridTarget = gridTargets.Dequeue();
            targetPos = playGrid.GetWorldPosition(gridTarget);

            setNextTarget = false;
        }
    }

    public void MoveToPoint()
    {
        
    }

    public void MoveAhead()
    {
        GridPosition lastGridPos = GetLastGridInRow(unit.CurGridPos.z);
        gridTargets.Enqueue(lastGridPos);
    }

    public void MoveUp()
    {
        GridPosition upGridPos = unit.CurGridPos + 1;

        gridTargets.Enqueue(upGridPos);

        GridPosition lastGridPos = GetLastGridInRow(upGridPos.z);
        gridTargets.Enqueue(lastGridPos);
    }

    public GridPosition GetLastGridInRow(int row)
    {
        return playGrid.GetLastGridInRow(row);
    }


    public override void Cancel()
    {
        if (transform.position.x < playGrid.GetWorldPosition(unit.CurGridPos).x)
        {
            targetPos = playGrid.GetWorldPosition(unit.CurGridPos);
        }
        else
        {
            //go to whatever grid pos it is supposed to be at
            
        }
    }

}
