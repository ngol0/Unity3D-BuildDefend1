using UnityEngine;
using Lam.DefenderBuilder.Tower;
using UnityEngine.Events;

public class InteractableTypeEventListener : MonoBehaviour
{
    public InteractableTypeEvent Event;
    public MyInteractableDataEvent UnitEventResponse;

    public void OnEventRaised(InteractableData item)
    {
        UnitEventResponse?.Invoke(item);
    }

    private void OnEnable()
    { Event.RegisterListener(this); }

    private void OnDisable()
    { Event.UnregisterListener(this); }
}

[System.Serializable]
public class MyInteractableDataEvent : UnityEvent<InteractableData>
{
}

