using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    [SerializeField] PlayGrid playGrid;
    [SerializeField] GridItemUI pathNodePrefab;
    GridSystem<PathNode> pathSystem;
    public GridSystem<PathNode> PathSystem => pathSystem;

    private const int STRAIGHT_MOVE_COST = 10;
    private const int DIAGONAL_COST = 14;

    private void Awake()
    {
        //delegate using anonymous function
        pathSystem = new GridSystem<PathNode>
            (playGrid.GridWidth, playGrid.GridHeight, playGrid.CellSize,
                delegate (GridSystem<PathNode> g, GridPosition gridPos)
                {
                    return new PathNode(g, gridPos);
                }
            );

        pathSystem.CreateGridUI(pathNodePrefab, transform);
    }

    //a-star algorithm
    public List<GridPosition> FindPath(GridPosition startPos, GridPosition endPos)
    {
        List<PathNode> openList = new List<PathNode>();
        List<PathNode> closedList = new List<PathNode>();

        PathNode startNode = GetNode(startPos);
        PathNode endNode = GetNode(endPos);

        openList.Add(startNode);

        ResetPathfindingData();

        startNode.SetGCost(0);
        startNode.SetHCost(CalculateDistance(startPos, endPos));
        startNode.CalculateFCost();

        while (openList.Count > 0)
        {
            PathNode currentNode = GetNodeWithLowestFCost(openList);
            if (closedList.Contains(currentNode)) //if already set current
            {
                continue;
            }

            if (currentNode == endNode)
            {
                //Debug.Log("reached here???");
                return CalculatePath(currentNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            //look at all neighbors
            foreach (var neighborNode in GetNeighbourList(currentNode))
            {
                if (closedList.Contains(neighborNode)) continue;

                int currentGCost = currentNode.GCost + 
                    CalculateDistance(currentNode.GridPos, neighborNode.GridPos);

                if (currentGCost < neighborNode.GCost)
                {
                    //reset g h and f cost
                    neighborNode.SetGCost(currentGCost);
                    neighborNode.SetHCost(CalculateDistance(neighborNode.GridPos, endPos));
                    neighborNode.CalculateFCost();
                    neighborNode.SetCameFromNode(currentNode);

                    if (!openList.Contains(neighborNode)) 
                    {
                        openList.Add(neighborNode);
                    }
                }
            }
        }

        return null;
    }

    private List<GridPosition> CalculatePath(PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();
        path.Add(endNode);

        while (endNode.CameFromNode != null)
        {
            path.Add(endNode.CameFromNode);
            endNode = endNode.CameFromNode;
        }
        
        path.Reverse();

        List<GridPosition> gridPosList = new List<GridPosition>();
        foreach (var item in path)
        {
            gridPosList.Add(item.GridPos);
        }

        return gridPosList;
    }

    private void ResetPathfindingData()
    {
        for (int x = 0; x < playGrid.GridWidth; x++)
        {
            for (int z = 0; z < playGrid.GridHeight; z++)
            {
                GridPosition gridPos = new GridPosition(x, z);
                PathNode node = GetNode(gridPos);
                node.SetCameFromNode(null);
                node.SetGCost(int.MaxValue);
                node.SetHCost(0);
                node.CalculateFCost();
            }
        }
    }

    private PathNode GetNodeWithLowestFCost(List<PathNode> listNodes)
    {
        PathNode lowestNode = listNodes[0];
        for (int i = 1; i < listNodes.Count; i++)
        {
            if (listNodes[i].CalculateFCost() < lowestNode.CalculateFCost())
            {
                lowestNode = listNodes[i];
            }
        }
        return lowestNode;
    }

    //diagnoal distance function for heuristics cost (HCost) & GCost
    private int CalculateDistance(GridPosition currentPos, GridPosition endPos)
    {
        int dx = Mathf.Abs(currentPos.x - endPos.x);
        int dy = Mathf.Abs(currentPos.z - endPos.z);

        return STRAIGHT_MOVE_COST*(dx+dy) + (DIAGONAL_COST - 2*STRAIGHT_MOVE_COST)*Mathf.Min(dx,dy);
    }

    private List<PathNode> GetNeighbourList(PathNode currentNode)
    {
        List<PathNode> neighbourList = new List<PathNode>();

        GridPosition gridPosition = currentNode.GridPos;

        for (int x = -1; x <=1; x++)
        {
            for (int z = -1; z <=1; z++)
            {
                if (x == 0 && z == 0) continue;
                GridPosition potentialNeighbor = gridPosition + new GridPosition(x,z);

                if (pathSystem.IsValidGridPos(potentialNeighbor))
                {
                    neighbourList.Add(GetNode(potentialNeighbor));
                }
            }
        }

        return neighbourList;
    }

    public PathNode GetNode(GridPosition gridPos) => pathSystem.GetGridItem(gridPos);
    public Vector3 GetWorldPosition(GridPosition gridPos) => pathSystem.GetWorldPosition(gridPos);
    public GridPosition GetGridPosition(Vector3 worldPos) => pathSystem.GetGridPosition(worldPos);
}
