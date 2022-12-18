using System;
using UnityEngine;


[CreateAssetMenu(menuName = "Events/String Event Channel")]
public class StringEventChannel : ScriptableObject
{
    public Action<string> OnEventRaised;

    public void RaiseEvent(string str)
    {
#if UNITY_EDITOR
        if (OnEventRaised == null)
            Debug.LogWarning($"No one listening to this event: {name}");
#endif
        OnEventRaised?.Invoke(str);
    }
}
