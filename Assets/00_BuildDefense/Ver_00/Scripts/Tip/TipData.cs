using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lam.DefenderBuilder
{
    [System.Serializable]
    public class TipItem
    {
        public int id;
        public string tipTitle;
        [TextArea(3,30)] public string tipContent;
        public Sprite tipImage;
    }

    [CreateAssetMenu(menuName = "ScriptableObjects/Tip Data")]
    public class TipData : ScriptableObject
    {
        public List<TipItem> tipList = new List<TipItem>();
    }
}

