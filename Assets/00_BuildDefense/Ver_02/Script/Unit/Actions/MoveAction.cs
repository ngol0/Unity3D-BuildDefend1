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
    private List<GridPosition> paths = new List<GridPosition>();

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

    public void SetPath(List<GridPosition> positionTargets)
    {
        foreach (var position in positionTargets)
        {
            gridTargets.Enqueue(position);
        }
    }

    public void MoveAhead(Pathfinding pathfinding)
    {
        GridPosition destination = GetLastGridInRow(unit.CurGridPos.z);
        paths = pathfinding.FindPath(unit.CurGridPos, destination);

        SetPath(paths);
    }

    public void MoveUp()
    {
        
    }

    public GridPosition GetLastGridInRow(int row)
    {
        return playGrid.GetLastGridInRow(row);
    }

    public void Cancel()
    {
        gridTargets.Clear();
    }

}
