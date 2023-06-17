using UnityEngine;
using Lam.DefenderBuilder.Tower;
using UnityEngine.Events;

public class TowerTypeEventListener : MonoBehaviour
{
    public TowerTypeEvent Event;
    public MyTowerEvent UnitEventResponse;

    public void OnEventRaised(HouseData item)
    {
        UnitEventResponse?.Invoke(item);
    }

    private void OnEnable()
    { Event.RegisterListener(this); }

    private void OnDisable()
    { Event.UnregisterListener(this); }
}

[System.Serializable]
public class MyTowerEvent : UnityEvent<HouseData>
{
}

