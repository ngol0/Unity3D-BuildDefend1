using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : BaseAction
{
    private Vector3 targetPos;
    private Unit unit;
    private float moveSpeed = 2f;
    private Vector3 moveDirection;
    private PlayGrid playGrid;
    private Queue<GridPosition> gridTargets = new();
    private List<GridPosition> paths = new();
    private Pathfinding pathfinding;


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
            transform.position += moveSpeed * Time.deltaTime * moveDirection;

            unit.animatorController.SetBool("isWalking", true);
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
            if (pathfinding.IsNodeWalkable(gridTarget))
            {
                targetPos = playGrid.GetWorldPosition(gridTarget);
                setNextTarget = false;

                //when target is set -> set current grid pos for unit
                playGrid.ItemMoveGridPosition(unit, unit.CurGridPos, gridTarget);
                pathfinding.ItemMoveGridPosition(unit, unit.CurGridPos, gridTarget);
                unit.SetCurrentGridPos(gridTarget);
            }
            else
            {
                Cancel();
            }
        }
        else if (gridTargets.Count == 0)
        {
            unit.animatorController.SetBool("isWalking", false);
        }
    }

    public void SetPath(List<GridPosition> positionTargets)
    {
        if (positionTargets == null) 
        {
            Debug.Log(":::Can't find path???");
            return;
        }
        
        for (int i = 1; i < positionTargets.Count; i++)
        {
            gridTargets.Enqueue(positionTargets[i]);
        }
    }

    public void TakeAction(Pathfinding pathfinding)
    {
        this.pathfinding = pathfinding;
        GridPosition destination = GetDestination(unit.CurGridPos);
        Debug.Log(":::Destination set: " + destination.x + ", " + destination.z);
        paths = pathfinding.FindPath(unit.CurGridPos, destination);

        SetPath(paths);
    }

    public GridPosition GetDestination(GridPosition curGridPos)
    {
        //get the farthest available tile in row
        for (int i = playGrid.gridStats.gridWidth - 1; i > curGridPos.x; i--)
        {
            GridPosition gridPos = new GridPosition(i, curGridPos.z);
            if (pathfinding.GetNode(gridPos).IsWalkable())
            {
                return gridPos;
            }
        }
        return curGridPos;
    }

    public void Cancel()
    {
        gridTargets.Clear();
    }
}
