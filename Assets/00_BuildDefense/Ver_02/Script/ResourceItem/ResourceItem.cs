using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceItem : IInteractable
{
    private bool isMoveable;
    public override bool IsMoveable => false;
    
    [SerializeField] ResourceItemData data;
    public ResourceItemData Data => data;
}
