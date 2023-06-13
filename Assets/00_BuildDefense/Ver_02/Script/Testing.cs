using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] Pathfinding pathFinding;
    [SerializeField] LayerMask gridLayer;

    List<GridPosition> list;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitData;
            if (Physics.Raycast(ray, out hitData, float.MaxValue, gridLayer))
            {
                GridPosition gridPos = pathFinding.GetGridPosition(hitData.point);
                list = pathFinding.FindPath(new GridPosition(0,0), gridPos);

                for (int i = 0; i<list.Count -1; i++)
                {
                    Debug.DrawLine(
                        pathFinding.GetWorldPosition(list[i]), 
                        pathFinding.GetWorldPosition(list[i + 1]), 
                        Color.blue, 
                        5f);

                    //make the character go there
                    
                }
            }
        }

        // if (Input.GetKeyDown(KeyCode.A))
        // {
        //     Debug.Log(list.Count);
        // }
    }
}
