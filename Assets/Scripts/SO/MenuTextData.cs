using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/MenuTextData")]
public class MenuTextData : ScriptableObject
{
    [Header("GameOver Menu")]
    public string winTitle;
    [TextArea(3,10)] public string winNoti;
    public string loseTitle;
    [TextArea(3,10)] public string loseNoti;
}
