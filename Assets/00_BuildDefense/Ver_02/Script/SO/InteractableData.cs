using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableData : ScriptableObject
{
    public string nameString; //name of tower

    public IInteractable housePrefab; //prefab of tower to init
    //public GameObject ghostPrefab; //prefab of tower to init
    public Sprite sprite;
}
