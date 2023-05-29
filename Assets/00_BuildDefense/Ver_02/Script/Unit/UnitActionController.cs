using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionController : MonoBehaviour
{
    Unit selectedUnit;

    public void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;
    }

    public void MoveUnit()
    {
        selectedUnit.Move();
    }
}
