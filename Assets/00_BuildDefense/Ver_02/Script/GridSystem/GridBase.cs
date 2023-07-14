using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GridBase : MonoBehaviour
{
    [SerializeField] protected GridStats gridStats;

    protected void InitialSetUp()
    {
        for (int x = 0; x < gridStats.gridWidth; x++)
        {
            for (int z = 0; z < gridStats.gridHeight; z++)
            {
                GridPosition gridPos = new GridPosition(x,z);
                RaycastHit hitData;
                if (Physics.Raycast(
                    GetWorldPosition(gridPos) + Vector3.down * 10f, Vector3.up, out hitData, 20f, gridStats.obstacleMask)) 
                {
                    if (hitData.transform.TryGetComponent<IGameItem>(out IGameItem item))
                    {
                        SetItemAtGrid(item, gridPos);
                    }
                }
            }
        }
    }

    public abstract GridPosition GetGridPosition(Vector3 worldPos);
    public abstract Vector3 GetWorldPosition(GridPosition gridPos);
    public abstract void SetItemAtGrid(IGameItem item, GridPosition gridPos);
}
