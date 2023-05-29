using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lam.DefenderBuilder.Resource;

namespace Lam.DefenderBuilder.Tower
{
    public class ResourceGenerator : MonoBehaviour
    {
        private float timer;
        private float maxTimer;

        int amountOfResource = 1;

        [SerializeField] ResourceTypeSO resourceType;
        private ResourceGeneratorData resourceGeneratorData;

        public void SetMaxTimer(float maxTimer)
        {
            if (maxTimer == -1) 
            {
                enabled = false;
                return;
            }
            this.maxTimer = maxTimer;
        }

        //increase the amount of resource after a certain amt of time (i.e: 1sec)
        void Update()
        {
            timer -= Time.deltaTime;
            if (timer < Mathf.Epsilon)
            {
                ResourceManager.Instance.AddResource(resourceType, amountOfResource);
                timer += maxTimer;
            }
        }
    }

}
