using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lam.DefenderBuilder.Tower;
using Lam.DefenderBuilder.Events;

namespace Lam.DefenderBuilder.UI
{
    public class TowerButtonItem : MonoBehaviour
    {
        [Header("UI ref")]
        [SerializeField] Image icon;
        [SerializeField] RectTransform iconRectTrans;
        [SerializeField] RectTransform btnRectTransform;
        [SerializeField] GameObject selectedSprite;
        [SerializeField] Sprite arrowUI;
        [SerializeField] MouseEnterEvent mouseHover;
        [Header("Event to raise")]
        [SerializeField] VoidEventChannel onButtonClicked;
        [SerializeField] ShowTooltipEventChannel showToolTip;
        [SerializeField] VoidEventChannel hideToolTip;

        TowerTypeSO buttonTowerType;

        private void Awake()
        {
            SetSelectedUIActive(false);
        }

        public void SetButtonUI(TowerTypeSO towerType, float offset, int index)
        {
            buttonTowerType = towerType;

            if (buttonTowerType != null)
            {
                icon.sprite = towerType.sprite;
            }
            else
            {
                icon.sprite = arrowUI;
                iconRectTrans.sizeDelta = new Vector2(0, -40);
            }

            btnRectTransform.anchoredPosition = new Vector3(offset * index, 0, 0);

            mouseHover.onMouseEnterEvent += ShowToolTip;
            mouseHover.onMouseExitEvent += HideToolTip;
        }

        public void OnButtonClicked()
        {
            TowerManager.Instance.SetTowerType(buttonTowerType);
            onButtonClicked?.RaiseEvent();
            SetSelectedUIActive(true);
        }

        public void SetSelectedUIActive(bool active)
        {
            selectedSprite.SetActive(active);
        }

        private void OnDisable() {
            mouseHover.onMouseEnterEvent -= ShowToolTip;
            mouseHover.onMouseExitEvent -= HideToolTip;
        }

        private void ShowToolTip()
        {
            if (buttonTowerType == null) return;
            showToolTip?.RaiseEvent(buttonTowerType.name + buttonTowerType.GetTowerResourceInfo(), null);
        }

        private void HideToolTip()
        {
            hideToolTip?.RaiseEvent();
        }
    }
}

