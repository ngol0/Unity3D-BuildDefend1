using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lam.DefenderBuilder.Resource;
using Lam.DefenderBuilder.UI;
using Lam.DefenderBuilder.Tower;

namespace Lam.DefenderBuilder
{
    public class Waypoints : MonoBehaviour
    {
        [Header("Condition")]
        [SerializeField] private bool isPlaceable;
        public bool IsPlaceable => isPlaceable;

        [Header("Event to raise")]
        [SerializeField] ShowTooltipEventChannel showToolTip;
        [SerializeField] VoidEventChannel hideToolTip;

        private TowerTypeSO activeTowerType;

        private float maxTimer;

        private void OnMouseOver()
        {
            activeTowerType = TowerManager.Instance.SelectedTowerType;

            if (activeTowerType == null) { return; }

            if (isPlaceable)
            {
                TowerGhost.Instance.SetTowerAndPlace(transform.position);

                if (TowerGhost.Instance.CanGenerateResource())
                    maxTimer = TowerGhost.Instance.GetMaxTimer();
                else
                    maxTimer = -1;
            }
        }

        private void OnMouseDown()
        {
            if (activeTowerType == null) { return; }

            if (isPlaceable)
            {
                if (ResourceManager.Instance.CanAfford(activeTowerType.resourceCostToBuild, out string errorMessage))
                {
                    var tower = Instantiate(activeTowerType.towerPrefab, transform.position, Quaternion.identity);
                    var resourceGenerator = tower.GetComponent<ResourceGenerator>();
                    if (resourceGenerator) resourceGenerator.SetMaxTimer(maxTimer);
                    //tower.GetComponent<HealthSystem>().OnDie += () => isPlaceable = true;
                    isPlaceable = false;

                    ResourceManager.Instance.SpendResource(activeTowerType.resourceCostToBuild);
                }
                else
                {
                    showToolTip?.RaiseEvent(errorMessage, new TooltipTimer {timerValue = 2f});
                }
            }
        }
    }
}

