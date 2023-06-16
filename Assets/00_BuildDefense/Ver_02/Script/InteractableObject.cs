using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour, IObject
{
    public abstract bool IsMoveable { get; }
    public abstract void SetGridData(PlayGrid grid);
}
