using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SOPlaceableObj", menuName = "PlaceableObj/Obj", order = 0)]
public class SOPlaceableObj : ScriptableObject 
{
    public string objName;
    public Interactable prefab;

    [TextArea(3,3)]
    public string description;
}
