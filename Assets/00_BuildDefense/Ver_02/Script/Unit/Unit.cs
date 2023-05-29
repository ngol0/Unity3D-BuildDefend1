using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : Interactable
{
    private void Update() 
    {
        
    }

    public void Move()
    {
        Debug.Log(name + "moving");
    }

    public override bool IsMoveable => true;
}
