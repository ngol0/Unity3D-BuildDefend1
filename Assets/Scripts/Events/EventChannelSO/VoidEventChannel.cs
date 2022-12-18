using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Events/Void Event Channel")]
public class VoidEventChannel : ScriptableObject
{
    public Action OnEventRaised;

    public void RaiseEvent()
    {
#if UNITY_EDITOR
        if (OnEventRaised == null)
        {
            Debug.LogWarning($"No one listen to this event {name}");
        }
#endif
        OnEventRaised?.Invoke();
    }
}
