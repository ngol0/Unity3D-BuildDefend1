using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNodeItem : MonoBehaviour, IGameItem
{
    public ResourceTypeSO type;

    public string GetName()
    {
        return gameObject.name;
    }
}
