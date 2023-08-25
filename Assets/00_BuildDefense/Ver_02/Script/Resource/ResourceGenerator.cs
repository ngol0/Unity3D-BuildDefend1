using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    private float timer;
    private float maxTimer = -1;

    int amountOfResource = 1;
    [SerializeField] AddResourceEvent OnAddResource;
    [SerializeField] ResourceTypeSO resourceType;

    private void Start() 
    {
        enabled = false;
    }

    public void SetMaxTimer()
    {
        enabled = true;
        maxTimer = GetComponent<ResourceGeneratorRepresentation>().MaxTimer;
    }

    //increase the amount of resource after a certain amt of time (i.e: 1sec)
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < Mathf.Epsilon)
        {
            if (OnAddResource) OnAddResource.Raise(resourceType, amountOfResource);
            timer += maxTimer;
        }
    }
}
