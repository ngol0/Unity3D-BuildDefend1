using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lam.DefenderBuilder.Resource;

namespace Lam.DefenderBuilder.Tower
{
    public class HeadResourceGenerator : ResourceGenerator
    {
        [Header("Reference")]
        [SerializeField] private ResourceGeneratorOverlay generatorOverlay;
        [SerializeField] HealthSystem healthSystem;
        [SerializeField] HealthBar healthBar;
        TowerTypeSO towerType;

        [Header("Event to listen to")]
        [SerializeField] IntEventChannel onEnemyFinishPath;

        private void OnEnable() 
        {
            if (onEnemyFinishPath) onEnemyFinishPath.OnEventRaised += OnEnemyFinishPath;
        }

        private void OnDisable() 
        {
            if (onEnemyFinishPath) onEnemyFinishPath.OnEventRaised -= OnEnemyFinishPath;
        }

        private void Start()
        {
            generatorOverlay.FindResourceNodeNearby();
            SetMaxTimer(generatorOverlay.MaxTimer);

            towerType = GetComponent<BuildingHolder>().towerType;
            SetHealth();
        }

        private void SetHealth()
        {
            if (healthSystem == null) return;

            healthSystem.OnDie += OnBuildingDisappear;
            healthSystem.SetHealthMax(towerType.healthAmountMax);
            healthBar.UpdateHealthbarUI(healthSystem.GetNormalizedHealthAmount());
        }

        private void OnBuildingDisappear()
        {
            Destroy(gameObject);
            GameManager.Instance.OnGameOver(false);
        }

        private void OnEnemyFinishPath(int hitPoint)
        {
            if (healthSystem) healthSystem.Damage(hitPoint);
            healthBar.UpdateHealthbarUI(healthSystem.GetNormalizedHealthAmount());
        }
    }
}
