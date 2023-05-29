using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Lam.DefenderBuilder.Resource;

namespace Lam.DefenderBuilder.Tower
{
    public class TowerGhost : SingletonMono<TowerGhost>
    {
        private GameObject ghostTower;

        TowerTypeListSO towerList;
        TowerTypeSO selectedTower;
        Vector3 positionToShow;

        ResourceGeneratorOverlay resourceGeneratorOverlay;

        private bool showGhost = false;

        Dictionary<TowerTypeSO, GameObject> towerGhostDict;

        public override void Awake()
        {
            Singleton();

            towerList = Resources.Load<TowerTypeListSO>(nameof(TowerTypeListSO));
            towerGhostDict = new Dictionary<TowerTypeSO, GameObject>();

            initGhostPrefab();
        }

        private void Start()
        {
            TowerManager.Instance.OnActiveTowerChange += TowerManager_OnActiveTowerChange;
        }

        //listen to whenever tower type is changed
        private void TowerManager_OnActiveTowerChange(object sender, TowerManager.OnActiveTowerChangeArgs e)
        {
            if (e.activeTowerType == null) //if tower type is null, shown ghost is false
            {
                showGhost = false;
            }
            else //else, set tower type to show
            {
                selectedTower = e.activeTowerType;
                resourceGeneratorOverlay = towerGhostDict[selectedTower].GetComponent<ResourceGeneratorOverlay>();
                if (resourceGeneratorOverlay) resourceGeneratorOverlay.FindResourceNodeNearby();
            }
        }

        private void Update()
        {
            if (showGhost) //if tower is chosen - shown ghost
            {
                Show(positionToShow);
            }
            else //else, when switch back to arrow, hide all
            {
                HideAll();
            }
        }

        private void HideAll()
        {
            foreach (var item in towerGhostDict) //first, hide all
            {
                item.Value.gameObject.SetActive(false);
            }
        }

        //set the prefab and position of tower to update - get values from waypoint script
        public void SetTowerAndPlace(Vector3 position)
        {
            showGhost = true;
            positionToShow = position;
        }

        public bool CanGenerateResource()
        {
            if (resourceGeneratorOverlay)
            {
                return resourceGeneratorOverlay.FindResourceNodeNearby();
            }
            return false;
        }

        public float GetMaxTimer()
        {
            if (resourceGeneratorOverlay)
                return resourceGeneratorOverlay.MaxTimer;

            return -1;
        }

        //show ghost prefab everytime mouse hover over a placeable waypoint
        public void Show(Vector3 position)
        {
            HideAll();

            towerGhostDict[selectedTower].SetActive(true); //then, turn the active one on
            transform.position = position; //update the position
        }

        //instantiate ghost prefab in scene
        private void initGhostPrefab()
        {
            foreach (TowerTypeSO towerType in towerList.list)
            {
                ghostTower = Instantiate(towerType.ghostPrefab, transform);
                ghostTower.SetActive(false);
                towerGhostDict[towerType] = ghostTower;
            }
        }
    }
}

