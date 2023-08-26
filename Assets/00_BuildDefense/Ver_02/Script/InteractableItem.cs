using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableItem : MonoBehaviour, IGameItem
{
    protected PlayGrid playGrid;
    public PlayGrid PlayGrid => playGrid;

    protected GridPosition curGridPos;
    public GridPosition CurGridPos => curGridPos;
    public InteractableData itemData;

    public virtual void SetGridData(PlayGrid gridSystem, Pathfinding pathGrid = null)
    {
        playGrid = gridSystem;
        curGridPos = playGrid.GetGridPosition(transform.position);
    }

    public string GetName()
    {
        return gameObject.name;
    }
}
