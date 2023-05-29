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

    public void MoveUnit()
    {
        if (selectedUnit!=null) selectedUnit.Move();
    }

    public void StopUnit()
    {
        if (selectedUnit!=null) selectedUnit.Stop();
    }
}
