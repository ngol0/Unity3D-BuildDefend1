using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionController : MonoBehaviour
{
    Unit selectedUnit;

    public void SetSelectedUnit(InteractableObject item)
    {
        if (item != null && item.IsMoveable)
        {
            selectedUnit = item as Unit;
            return;
        }
        selectedUnit = null;
    }

    public void MoveStraight()
    {
        if (selectedUnit==null) return;
        selectedUnit.GetAction<MoveAction>().MoveAhead();
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
