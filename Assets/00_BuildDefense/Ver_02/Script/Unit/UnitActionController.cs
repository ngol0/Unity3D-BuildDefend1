using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionController : MonoBehaviour
{
    Unit selectedUnit;
    [SerializeField] Pathfinding pathfinding;

    public void SetSelectedUnit(IInteractable item)
    {
        if (item != null && item.IsMoveable)
        {
            selectedUnit = item as Unit;
            return;
        }
        selectedUnit = null;
    }

    public void MoveAhead()
    {
        if (selectedUnit==null) return;
        selectedUnit.GetAction<MoveAction>().MoveAhead(pathfinding);
    }

    public void StopUnit()
    {
        if (selectedUnit==null) return; 
        selectedUnit.GetAction<MoveAction>().Cancel();
    }

    public void MoveUp()
    {
        if (selectedUnit==null) return;
        selectedUnit.GetAction<MoveAction>().MoveUp();
    }
}
