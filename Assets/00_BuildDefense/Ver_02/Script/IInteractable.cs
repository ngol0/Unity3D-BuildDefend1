using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IInteractable : MonoBehaviour, IObject
{
    protected PlayGrid playGrid;
    public PlayGrid PlayGrid => playGrid;
    public abstract bool IsMoveable { get; }

    public virtual void SetGridData(PlayGrid grid)
    {
        this.playGrid = grid;
    }
}
