using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableEventListener : MonoBehaviour
{
    public InteractableEvent Event;
    public MyInteractable UnitEventResponse;

    public void OnEventRaised(InteractableObject item)
    {
        UnitEventResponse?.Invoke(item);
    }

    private void OnEnable()
    { Event.RegisterListener(this); }

    private void OnDisable()
    { Event.UnregisterListener(this); }
}

[System.Serializable]
public class MyInteractable : UnityEvent<InteractableObject>
{
}

