using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionController : MonoBehaviour
{
    Unit selectedUnit;
    [SerializeField] Pathfinding pathfinding;

    public System.Action<bool> OnSelectedUnit;

    public void SetSelectedUnit(IInteractable item)
    {
        if (item is Unit)
        {
            selectedUnit = item as Unit;
        }
        else
        {
            selectedUnit = null;
        }
        OnSelectedUnit?.Invoke(selectedUnit!=null);
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
