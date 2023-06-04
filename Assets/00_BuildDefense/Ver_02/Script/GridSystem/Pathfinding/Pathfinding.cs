using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    [SerializeField] GridManager gridManager;
    [SerializeField] GridItemUI pathNodePrefab;
    Grid<PathNode> pathSystem;
    public Grid<PathNode> PathSystem => pathSystem;

    private const int STRAIGHT_MOVE_COST = 10;
    private const int DIAGONAL_COST = 14;

    private void Awake()
    {
        //delegate using anonymous function
        pathSystem = new Grid<PathNode>
            (gridManager.GridWidth, gridManager.GridHeight, gridManager.CellSize,
                delegate (Grid<PathNode> g, GridPosition gridPos)
                {
                    return new PathNode(g, gridPos);
                }
            );

        pathSystem.CreateGridUI(pathNodePrefab, transform);
    }

    public List<GridPosition> FindPath(PathNode startNode, PathNode endNode)
    {
        List<PathNode> openList = new List<PathNode>();
        List<PathNode> closedList = new List<PathNode>();

        openList.Add(startNode);

        ResetPathfindingData();

        startNode.SetGCost(0);
        startNode.SetHCost(GetDiagonalDistance(startNode.GridPos, endNode.GridPos));
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
                    GetDiagonalDistance(currentNode.GridPos, neighborNode.GridPos);

                if (currentGCost < neighborNode.GCost)
                {
                    //reset g h and f cost
                    neighborNode.SetGCost(currentGCost);
                    neighborNode.SetHCost(GetDiagonalDistance(currentNode.GridPos, endNode.GridPos));
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
        for (int x = 0; x < gridManager.GridWidth; x++)
        {
            for (int z = 0; z < gridManager.GridHeight; z++)
            {
                GridPosition gridPos = new GridPosition(x, z);
                PathNode node = pathSystem.GetGridItem(gridPos);
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
    private int GetDiagonalDistance(GridPosition currentPos, GridPosition endPos)
    {
        int dx = Mathf.Abs(currentPos.x = endPos.x);
        int dy = Mathf.Abs(currentPos.z = endPos.z);

        return STRAIGHT_MOVE_COST*(dx+dy) + (DIAGONAL_COST - 2*STRAIGHT_MOVE_COST)*Mathf.Min(dx,dy);
    }

    private List<PathNode> GetNeighbourList(PathNode currentNode)
    {
        List<PathNode> neighbourList = new List<PathNode>();

        GridPosition gridPosition = currentNode.GridPos;

        if (gridPosition.x - 1 >= 0)
        {
            // Left
            neighbourList.Add(GetNode(gridPosition.x - 1, gridPosition.z + 0));
            if (gridPosition.z - 1 >= 0)
            {
                // Left Down
                neighbourList.Add(GetNode(gridPosition.x - 1, gridPosition.z - 1));
            }

            if (gridPosition.z + 1 < gridManager.GridHeight)
            {
                // Left Up
                neighbourList.Add(GetNode(gridPosition.x - 1, gridPosition.z + 1));
            }
        }

        if (gridPosition.x + 1 < gridManager.GridWidth)
        {
            // Right
            neighbourList.Add(GetNode(gridPosition.x + 1, gridPosition.z + 0));
            if (gridPosition.z - 1 >= 0)
            {
                // Right Down
                neighbourList.Add(GetNode(gridPosition.x + 1, gridPosition.z - 1));
            }
            if (gridPosition.z + 1 < gridManager.GridHeight)
            {
                // Right Up
                neighbourList.Add(GetNode(gridPosition.x + 1, gridPosition.z + 1));
            }
        }

        if (gridPosition.z - 1 >= 0)
        {
            // Down
            neighbourList.Add(GetNode(gridPosition.x + 0, gridPosition.z - 1));
        }
        if (gridPosition.z + 1 < gridManager.GridHeight)
        {
            // Up
            neighbourList.Add(GetNode(gridPosition.x + 0, gridPosition.z + 1));
        }

        return neighbourList;
    }

    public PathNode GetNode(int a, int b)
    {
        return pathSystem.GetGridItem(new GridPosition(a,b));
    }

    public Vector3 GetWorldPosition(GridPosition gridPos) => pathSystem.GetWorldPosition(gridPos);
     public GridPosition GetGridPosition(Vector3 worldPos) => pathSystem.GetGridPosition(worldPos);
}
