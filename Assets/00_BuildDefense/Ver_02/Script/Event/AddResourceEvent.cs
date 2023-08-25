using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;

[CreateAssetMenu(menuName = "CustomizedEvents/ResourceEvent")]
public class AddResourceEvent : ScriptableObject
{
    private List<AddResourceEventListener> listeners =
        new List<AddResourceEventListener>();

    public void Raise(ResourceTypeSO resourceType, int resourceAmount)
    {
        //Debug.Log(":::Raise?");
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised(resourceType, resourceAmount);
    }
    
    public void RegisterListener(AddResourceEventListener listener)
    { listeners.Add(listener); }

    public void UnregisterListener(AddResourceEventListener listener)
    { listeners.Remove(listener); }
}
