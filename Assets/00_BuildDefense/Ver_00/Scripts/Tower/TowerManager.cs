using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Lam.DefenderBuilder.Tower
{
    public class TowerManager : SingletonMono<TowerManager>
    {
        //event handler
        public event EventHandler<OnActiveTowerChangeArgs> OnActiveTowerChange;

        public class OnActiveTowerChangeArgs : EventArgs
        {
            public TowerTypeSO activeTowerType;
        }

        //reference
        private TowerTypeSO selectedTowerType;
        public TowerTypeSO SelectedTowerType => selectedTowerType;

        //Method//
        //set tower type
        public void SetTowerType(TowerTypeSO towerType)
        {
            selectedTowerType = towerType;
            OnActiveTowerChange?.Invoke(this, new OnActiveTowerChangeArgs { activeTowerType = selectedTowerType });
        }
    }
}

