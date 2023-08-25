using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;
using UnityEngine.Events;


public class AddResourceEventListener : MonoBehaviour
{
    public AddResourceEvent Event;
    public MyResourceEvent UnitEventResponse;

    public void OnEventRaised(ResourceTypeSO resourceType, int resourceAmount)
    {
        UnitEventResponse?.Invoke(resourceType, resourceAmount);
    }

    private void OnEnable()
    { Event.RegisterListener(this); }

    private void OnDisable()
    { Event.UnregisterListener(this); }
}

[System.Serializable]
public class MyResourceEvent : UnityEvent<ResourceTypeSO, int>
{
}

