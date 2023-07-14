using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceItem : InteractableItem
{   
    [SerializeField] ResourceItemData data;
    public ResourceItemData Data => data;
}
