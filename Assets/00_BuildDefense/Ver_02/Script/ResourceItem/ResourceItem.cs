using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceItem : InteractableItem
{
    public override void SetGridData(PlayGrid gridSystem)
    {
        base.SetGridData(gridSystem);
        Debug.Log(":::Resource item asked to be placed");
    }
}
