using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lam.DefenderBuilder.Tower;

public class HouseSelectionUI : MonoBehaviour
{
    [SerializeField] HouseButtonUI buttonPrefab;
    [SerializeField] HouseDataList listOfTower;
    [SerializeField] Transform rootSpawn;

    private void Awake() 
    {
        foreach (var item in listOfTower.list)
        {
            var btn = Instantiate<HouseButtonUI>(buttonPrefab, rootSpawn);
            btn.SetTowerData(item);
        }
    }
}
