using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Events/Int Event Channel")]
public class IntEventChannel : ScriptableObject
{
    public Action<int> OnEventRaised;

    public void RaiseEvent(int integer)
    {
#if UNITY_EDITOR
        if (OnEventRaised == null)
        {
            Debug.LogWarning($"No one listen to this event {name}");
        }
#endif
        OnEventRaised?.Invoke(integer);
    }
}
