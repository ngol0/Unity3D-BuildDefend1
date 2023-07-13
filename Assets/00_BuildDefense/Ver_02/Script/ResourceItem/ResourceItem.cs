using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceItem : IInteractable
{   
    [SerializeField] ResourceItemData data;
    public ResourceItemData Data => data;
}
