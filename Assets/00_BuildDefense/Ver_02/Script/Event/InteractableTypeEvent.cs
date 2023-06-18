using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lam.DefenderBuilder.Tower;

[CreateAssetMenu(menuName = "CustomizedEvents/HouseTypeEvent")]
public class InteractableTypeEvent : ScriptableObject
{
    private List<InteractableTypeEventListener> listeners =
        new List<InteractableTypeEventListener>();

    public void Raise(InteractableData unit)
    {
        //Debug.Log(":::Raise?");
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised(unit);
    }
    
    public void RegisterListener(InteractableTypeEventListener listener)
    { listeners.Add(listener); }

    public void UnregisterListener(InteractableTypeEventListener listener)
    { listeners.Remove(listener); }
}
