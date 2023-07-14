using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LoadingItem : MonoBehaviour
{
    Dictionary<PlaceableButtonUI, float> itemDict = new Dictionary<PlaceableButtonUI, float>();

    public void StartLoading(PlaceableButtonUI item, float coolDownTime)
    {
        itemDict[item] = coolDownTime;
    }

    public float GetLoadingTime(PlaceableButtonUI item)
    {
        if (!itemDict.ContainsKey(item)) return 0;
        return itemDict[item];
    }

    public float GetRemainingTime(PlaceableButtonUI item)
    {
        if (!itemDict.ContainsKey(item) || item == null) return 0;
        return itemDict[item]/item.ItemData.loadingTime;
    }

    private void Update() 
    {
        foreach (var item in itemDict.Keys.ToList())
        {
            itemDict[item] -= Time.deltaTime;
            if (itemDict[item] <= 0)
            {
                itemDict.Remove(item);
            }
        }
    }
}
