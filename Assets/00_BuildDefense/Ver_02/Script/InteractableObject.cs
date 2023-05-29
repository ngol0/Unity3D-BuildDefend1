using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public abstract bool IsMoveable { get; }
    public abstract void SetGridSystem(GridManager grid, GridPosition gridPosition);
}
