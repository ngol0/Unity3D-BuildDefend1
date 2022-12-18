using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lam.DefenderBuilder.Tower
{
    [CreateAssetMenu(menuName = "ScriptableObjects/TowerTypeList")]
    public class TowerTypeListSO : ScriptableObject
    {
        public List<TowerTypeSO> list;
    }
}

