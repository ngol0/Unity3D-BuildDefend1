using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Lam.DefenderBuilder.UI
{
    public class TooltipUI : MonoBehaviour
    {
        [Header("UI Ref")]
        [SerializeField] GameObject guiMain;
        [SerializeField] TextMeshProUGUI warningText;
        [SerializeField] RectTransform backgroundRect;
        [SerializeField] RectTransform thisRectTransfrom;
        [SerializeField] RectTransform canvasRectTransform;

        [Header("Event to listen to")]
        [SerializeField] ShowTooltipEventChannel showToolTip;
        [SerializeField] VoidEventChannel hideToolTip;

        private TooltipTimer timer;

        private void OnEnable()
        {
            if (hideToolTip) hideToolTip.OnEventRaised += Hide;
            if (showToolTip) showToolTip.OnEventRaised += Show;
        }

        private void OnDisable()
        {
            if (hideToolTip) hideToolTip.OnEventRaised -= Hide;
            if (showToolTip) showToolTip.OnEventRaised -= Show;
        }

        private void Start()
        {
            Hide();
        }

        private void SetText(string toolTipText)
        {
            warningText.SetText(toolTipText);
        }

        private void Update()
        {
            Vector2 anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.x;

            if (anchoredPosition.x + backgroundRect.rect.width > canvasRectTransform.rect.width)
                anchoredPosition.x = canvasRectTransform.rect.width - backgroundRect.rect.width;

            if (anchoredPosition.y + backgroundRect.rect.height > canvasRectTransform.rect.height)
                anchoredPosition.y = canvasRectTransform.rect.height - backgroundRect.rect.height;

            thisRectTransfrom.anchoredPosition = anchoredPosition;

            if (timer != null)
            {
                timer.timerValue -= Time.deltaTime;
                if (timer.timerValue <= 0)
                {
                    Hide();
                }
            }
        }

        public void Show(string toolTipText, TooltipTimer timer = null)
        {
            SetText(toolTipText);
            guiMain.SetActive(true);

            this.timer = timer;
        }

        public void Hide()
        {
            guiMain.SetActive(false);
        }
    }

    public class TooltipTimer
    {
        public float timerValue;
    }
}

