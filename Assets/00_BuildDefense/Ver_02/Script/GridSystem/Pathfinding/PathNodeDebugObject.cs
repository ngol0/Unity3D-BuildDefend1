using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNodeDebugObject : GridItemUI
{
    PathNode node;
    [SerializeField] TextMesh fCost;
    [SerializeField] TextMesh gCost;
    [SerializeField] TextMesh hCost;

    public override void SetGridItem(object gridItem)
    {
        base.SetGridItem(gridItem);
        node = gridItem as PathNode;
    }

    protected override void Update()
    {
        base.Update();
        fCost.text = node.FCost.ToString();
        gCost.text = node.GCost.ToString();
        hCost.text = node.HCost.ToString();
    }   
}
