using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lam.DefenderBuilder.Resource;

namespace Lam.DefenderBuilder.Tower
{
    [CreateAssetMenu(menuName = "ScriptableObjects/TowerType")]
    public class TowerTypeSO : ScriptableObject
    {
        public string nameString; //name of tower

        public GameObject towerPrefab; //prefab of tower to init
        public GameObject ghostPrefab; //prefab of tower to init

        public ResourceGeneratorData resourceData; //number of resource to build tower

        public Sprite sprite;
        public ResourceAmount[] resourceCostToBuild;
        public int healthAmountMax;

        public string GetTowerResourceInfo()
        {
            string str = "";
            foreach (var resource in resourceCostToBuild)
            {
                str += "<color=#" + resource.resourceType.colorHex + ">" + "\n" +
                resource.resourceType.shortName + ": " + resource.amount;
            }
            return str;
        }

    }
}

