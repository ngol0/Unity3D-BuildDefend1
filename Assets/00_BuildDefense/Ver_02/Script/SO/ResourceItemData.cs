using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lam.DefenderBuilder.Resource;

[CreateAssetMenu(menuName = "InteractableData/HouseType")]
public class ResourceItemData : InteractableData
{
    public ResourceGeneratorData resourceData; //number of resource to build tower
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

    public InteractableData[] itemToSell;
}
