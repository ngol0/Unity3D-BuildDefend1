using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Lam.DefenderBuilder.Resource;

namespace Lam.DefenderBuilder.UI
{
    public class ResourceUI : MonoBehaviour
    {
        [SerializeField] GameObject resourceTemplate;

        GameObject resourceUI;
        ResourceTypeListSO resourceList;
        Dictionary<ResourceTypeSO, GameObject> resourceUIDictionary;

        private void Awake()
        {
            resourceTemplate.SetActive(false);
            resourceUIDictionary = new Dictionary<ResourceTypeSO, GameObject>();
            resourceList = Resources.Load<ResourceTypeListSO>(nameof(ResourceTypeListSO));
        }

        private void Start()
        {
            CloneTemplate();
            UpdateResourceAmount();

            //event listener
            ResourceManager.Instance.OnResourceAmountChange += ResourceManager_OnResourceAmountChange;
        }

        private void ResourceManager_OnResourceAmountChange(object sender, System.EventArgs e)
        {
            UpdateResourceAmount();
        }

        private void CloneTemplate()
        {
            int index = 0;
            float offset = -160;

            foreach (ResourceTypeSO resourceType in resourceList.list)
            {
                //init object
                resourceUI = Instantiate(resourceTemplate, transform);
                resourceUI.SetActive(true);

                //set anchor position
                resourceUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(index * offset, 0);

                //set sprite
                resourceUI.GetComponentInChildren<Image>().sprite = resourceType.sprite;

                //put values in dictionary
                resourceUIDictionary[resourceType] = resourceUI;

                index++;
            }
        }

        private void UpdateResourceAmount()
        {
            foreach (var item in resourceUIDictionary)
            {
                int resourceAmount = ResourceManager.Instance.GetResourceAmount(item.Key);
                item.Value.GetComponentInChildren<TextMeshProUGUI>().SetText(resourceAmount.ToString());
            }

            Debug.Log("ehhh???");
        }
    }
}

