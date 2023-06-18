using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats/GridStat")]
public class GridStats : ScriptableObject
{
    public int gridWidth = 10;
    public int gridHeight = 7;
    public int cellSize = 20;
    public LayerMask obstacleMask;
}
