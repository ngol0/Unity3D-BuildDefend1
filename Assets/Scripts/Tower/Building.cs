using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lam.DefenderBuilder.Tower
{
    public class Building : MonoBehaviour
    {
        [SerializeField] HealthSystem healthSystem;
        [SerializeField] TowerTypeSO towerType;

        private void Start() {
            if (healthSystem) healthSystem.OnDie += OnBuildingDisappear;

            if (healthSystem) healthSystem.SetHealthMax(towerType.healthAmountMax);
        }

        private void OnBuildingDisappear()
        {
            Destroy(gameObject);
        }
    }
}

