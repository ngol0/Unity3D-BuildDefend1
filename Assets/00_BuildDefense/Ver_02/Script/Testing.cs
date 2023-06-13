using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Testing : MonoBehaviour
{
    [SerializeField] Pathfinding pathFinding;
    [SerializeField] LayerMask gridLayer;

    public List<GridPosition> list;
    public bool isTesting = false;
    GridPosition startPos;
    public Action Move;

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isTesting)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitData;
            if (Physics.Raycast(ray, out hitData, float.MaxValue, gridLayer))
            {
                GridPosition gridPos = pathFinding.GetGridPosition(hitData.point);
                list = pathFinding.FindPath(startPos, gridPos);

                for (int i = 0; i<list.Count -1; i++)
                {
                    Debug.DrawLine(
                        pathFinding.GetWorldPosition(list[i]), 
                        pathFinding.GetWorldPosition(list[i + 1]), 
                        Color.blue, 
                        2f);

                }

                Move?.Invoke();
            }
        }
    }

    public void SetStartingPoint(GridPosition pos)
    {
        this.startPos = pos;
    }
}
