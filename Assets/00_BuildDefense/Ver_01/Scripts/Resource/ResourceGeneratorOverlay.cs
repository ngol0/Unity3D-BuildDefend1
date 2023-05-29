using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Lam.DefenderBuilder.Tower;

namespace Lam.DefenderBuilder.Resource
{
    public class ResourceGeneratorOverlay : MonoBehaviour
    {
        [SerializeField] TextMeshPro resourcesAmount;
        private float maxTimer;
        private bool nearByResourceNode;
        private ResourceTypeSO resourceType;
        private ResourceGeneratorData resourceGeneratorData;

        public float MaxTimer => maxTimer;

        private void Awake()
        {
            resourceGeneratorData = this.GetComponent<BuildingHolder>().towerType.resourceData; //resource data
            maxTimer = resourceGeneratorData.timerMax; //time max
            resourceType = resourceGeneratorData.resourceType; //resource type
        }

        private void UpdateResourceText()
        {
            if (resourcesAmount == null) return;
            if (GetResourceGeneratedPerSecond() != 0)
            {
                resourcesAmount.gameObject.SetActive(true);
                resourcesAmount.text = (GetResourceGeneratedPerSecond() < 0.5) ? "+" : "++";
            }
            else
            {
                resourcesAmount.gameObject.SetActive(false);
            }
        }

        public bool FindResourceNodeNearby()
        {
            int nearByResourceAmt = 0;
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, resourceGeneratorData.resourceDetectionRadius);
            foreach (Collider collider in colliderArray)
            {
                var resourceNodeComponent = collider.GetComponent<ResourceNode>();
                if (resourceNodeComponent && resourceNodeComponent.resourceType == resourceType)
                {
                    nearByResourceAmt++;
                }
            }
            nearByResourceAmt = Mathf.Clamp(nearByResourceAmt, 0, resourceGeneratorData.maxResourceAmount);

            if (nearByResourceAmt > 0)
            {
                nearByResourceNode = true;
                maxTimer = (resourceGeneratorData.timerMax / 2f) +
                    resourceGeneratorData.timerMax *
                    (1 - (float)nearByResourceAmt / resourceGeneratorData.maxResourceAmount);
            }
            else
            {
                nearByResourceNode = false;
            }
            //Debug.Log("Nearby Resource:" + nearByResourceAmt);
            UpdateResourceText();

            return nearByResourceNode;
        }

        public float GetResourceGeneratedPerSecond()
        {
            if (nearByResourceNode)
                return 1 / maxTimer;
            else
                return 0;
        }
    }
}

