using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lam.DefenderBuilder.Tower;

[CreateAssetMenu(menuName = "CustomizedEvents/HouseTypeEvent")]
public class TowerTypeEvent : ScriptableObject
{
    private List<TowerTypeEventListener> listeners =
        new List<TowerTypeEventListener>();

    public void Raise(HouseData unit)
    {
        //Debug.Log(":::Raise?");
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised(unit);
    }
    
    public void RegisterListener(TowerTypeEventListener listener)
    { listeners.Add(listener); }

    public void UnregisterListener(TowerTypeEventListener listener)
    { listeners.Remove(listener); }
}
