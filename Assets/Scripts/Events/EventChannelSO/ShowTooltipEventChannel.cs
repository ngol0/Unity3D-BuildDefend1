using System;
using UnityEngine;
using Lam.DefenderBuilder.UI;


[CreateAssetMenu(menuName = "Events/Tooltip/Show Tooltip Event Channel")]
public class ShowTooltipEventChannel : ScriptableObject
{
    public Action<string, TooltipTimer> OnEventRaised;

    public void RaiseEvent(string str, TooltipTimer timer)
    {
#if UNITY_EDITOR
        if (OnEventRaised == null)
            Debug.LogWarning($"No one listening to this event: {name}");
#endif
        OnEventRaised?.Invoke(str, timer);
    }
}
