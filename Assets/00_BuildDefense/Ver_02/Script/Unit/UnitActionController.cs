using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionController : MonoBehaviour
{
    Unit selectedUnit;

    public void SetSelectedUnit(Interactable item)
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
}
