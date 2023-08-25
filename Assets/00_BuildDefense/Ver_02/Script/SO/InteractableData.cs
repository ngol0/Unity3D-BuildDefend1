using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableData : ScriptableObject
{
    public string nameString; //name of tower

    public InteractableItem prefab; //prefab of tower to init
    //public GameObject ghostPrefab; //prefab of tower to init
    public Sprite sprite;
    public float loadingTime;

    public ResourceAmount[] resourceCostToBuild;
}
