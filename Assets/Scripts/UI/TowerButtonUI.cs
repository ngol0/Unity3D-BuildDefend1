using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lam.DefenderBuilder.Tower;

namespace Lam.DefenderBuilder.UI
{
    public class TowerButtonUI : MonoBehaviour
    {
        [Header("Ref")]
        [SerializeField] private TowerButtonItem btnUI;
        [Header("Event to listen to")]
        [SerializeField] private VoidEventChannel onButtonClicked;

        TowerButtonItem btn;
        TowerButtonItem arrowBtn;
        TowerTypeListSO towerList;

        private void Awake()
        {
            btnUI.gameObject.SetActive(false);
            towerList = Resources.Load<TowerTypeListSO>(nameof(TowerTypeListSO));

            CloneTemplate();
        }

        private void OnEnable() {
            if (onButtonClicked) onButtonClicked.OnEventRaised += UnselectAll;
        }

        private void OnDisable() {
            if (onButtonClicked) onButtonClicked.OnEventRaised -= UnselectAll;
        }

        private void Start() {
            TowerTypeSO selectedTowerType = TowerManager.Instance.SelectedTowerType;

            if (selectedTowerType == null)
                arrowBtn.SetSelectedUIActive(true);
        }

        private void CloneTemplate()
        {
            float offset = 115f;
            int index = 0;

            arrowBtn = Instantiate<TowerButtonItem>(btnUI, transform);
            arrowBtn.gameObject.SetActive(true);

            arrowBtn.SetButtonUI(null, offset, index);

            index++;

            foreach (TowerTypeSO towerType in towerList.list)
            {
                btn = Instantiate<TowerButtonItem>(btnUI, transform);
                btn.gameObject.SetActive(true);

                btn.SetButtonUI(towerType, offset, index);

                index++;
            }
        }

        public void UnselectAll()
        {
            foreach (Transform child in transform)
            {
                var uiComponent = child.GetComponent<TowerButtonItem>();
                if (uiComponent) uiComponent.SetSelectedUIActive(false);
            }
        }
    }
}

