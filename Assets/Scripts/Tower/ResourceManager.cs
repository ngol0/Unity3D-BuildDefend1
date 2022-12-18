using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Lam.DefenderBuilder.Resource
{
    public class ResourceManager : SingletonMono<ResourceManager>
    {
        [Header("Initial Resource amount")]
        [SerializeField] private List<ResourceAmount> initialResources;
        private Dictionary<ResourceTypeSO, int> resourceAmountDictionary;
        private ResourceTypeListSO resourceTypeList;

        public event EventHandler OnResourceAmountChange; //event handler for resource updates

        public override void Awake()
        {
            Singleton();

            resourceAmountDictionary = new Dictionary<ResourceTypeSO, int>(); //init dictionary
            resourceTypeList = Resources.Load<ResourceTypeListSO>(nameof(ResourceTypeListSO)); //getting data of resources types

            //initialize the dictionary
            foreach (ResourceTypeSO resource in resourceTypeList.list)
            {
                resourceAmountDictionary[resource] = 0; //putresource from resource list into dictionary
                                                        //set each resource in the resource type list to zero
            }

            foreach (ResourceAmount resource in initialResources)
            {
                AddResource(resource.resourceType, resource.amount);
            }
        }

        //add certain amount of resources into certain type of resource
        public void AddResource(ResourceTypeSO resource, int amount)
        {
            resourceAmountDictionary[resource] += amount;

            //fire resource amount event update
            OnResourceAmountChange?.Invoke(this, EventArgs.Empty); //equals to if event is not null, do this
        }

        public int GetResourceAmount(ResourceTypeSO resourceType)
        {
            return resourceAmountDictionary[resourceType];
        }

        public bool CanAfford(ResourceAmount[] resourceAmountArray, out string errorMessage)
        {
            foreach (var resourceAmount in resourceAmountArray)
            {
                if (GetResourceAmount(resourceAmount.resourceType) >= resourceAmount.amount)
                {
                    errorMessage = "";
                }
                else 
                {
                    errorMessage = "Not enough resource to build";
                    return false;
                }
            }
            //can afford all
            errorMessage = "";
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
}

