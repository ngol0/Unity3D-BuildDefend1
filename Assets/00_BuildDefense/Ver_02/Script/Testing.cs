using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] Pathfinding pathFinding;
    [SerializeField] LayerMask gridLayer;
    PathNode startingPathNode;
    PathNode endNode;

    List<GridPosition> list;
    
    void Start()
    {
        startingPathNode = pathFinding.GetNode(0,0);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitData;
            if (Physics.Raycast(ray, out hitData, float.MaxValue, gridLayer))
            {
                GridPosition gridPos = pathFinding.GetGridPosition(hitData.point);
                endNode = pathFinding.GetNode(gridPos.x, gridPos.z);
                list = pathFinding.FindPath(startingPathNode, endNode);

                for (int i = 0; i<list.Count -1; i++)
                {
                    Debug.DrawLine(
                        pathFinding.GetWorldPosition(list[i]), 
                        pathFinding.GetWorldPosition(list[i + 1]), 
                        Color.blue, 
                        5f);
                }
            }
        }

        // if (Input.GetKeyDown(KeyCode.A))
        // {
        //     Debug.Log(list.Count);
        // }
    }
}
