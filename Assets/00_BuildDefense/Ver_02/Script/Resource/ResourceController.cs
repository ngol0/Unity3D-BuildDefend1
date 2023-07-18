using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour
{
    [Header("Initial Resource amount")]
    [SerializeField] private List<ResourceAmount> initialResources;
    [SerializeField] public ResourceTypeListSO resourceTypeList;
    private Dictionary<ResourceTypeSO, int> resourceAmountDictionary;
    public System.Action OnResourceAmountChange; //event handler for resource updates

    private void Awake()
    {
        resourceAmountDictionary = new Dictionary<ResourceTypeSO, int>(); //init dictionary

        //initialize the dictionary
        foreach (ResourceTypeSO resource in resourceTypeList.list)
        {
            resourceAmountDictionary[resource] = 0;
        }

        foreach (ResourceAmount resource in initialResources)
        {
            AddResource(resource.resourceType, resource.amount);
        }
    }

    public void AddResource(ResourceTypeSO resource, int amount)
    {
        resourceAmountDictionary[resource] += amount;
        OnResourceAmountChange?.Invoke();
    }

    public int GetResourceAmount(ResourceTypeSO resourceType)
    {
        return resourceAmountDictionary[resourceType];
    }

    public bool CanAfford(ResourceAmount[] resourceAmountArray)
    {
        foreach (var resourceAmount in resourceAmountArray)
        {
            if (GetResourceAmount(resourceAmount.resourceType) < resourceAmount.amount)
            {
                return false;
            }
        }
        return true;
    }

    public void SpendResource(ResourceAmount[] resourceAmountArray)
    {
        foreach (var resourceAmount in resourceAmountArray)
        {
            resourceAmountDictionary[resourceAmount.resourceType] -= resourceAmount.amount;
        }
    }
}
