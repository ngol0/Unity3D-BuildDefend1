using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CustomizedEvents/InteractableEvent")]
public class InteractableEvent : ScriptableObject
{
    private List<InteractableEventListener> listeners =
        new List<InteractableEventListener>();

    public void Raise(InteractableItem unit)
    {
        //Debug.Log(":::Raise?");
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised(unit);
    }
    
    public void RegisterListener(InteractableEventListener listener)
    { listeners.Add(listener); }

    public void UnregisterListener(InteractableEventListener listener)
    { listeners.Remove(listener); }
}
