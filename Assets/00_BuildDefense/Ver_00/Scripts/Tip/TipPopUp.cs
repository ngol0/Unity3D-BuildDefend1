using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Lam.DefenderBuilder.UI
{
    public class TipPopUp : MonoBehaviour
    {
        [SerializeField] TipData tipData;

        [Header("UI")]
        [SerializeField] GameObject guiMain;
        [SerializeField] TextMeshProUGUI titleText;
        [SerializeField] TextMeshProUGUI contentText;
        [SerializeField] Image tipImg;
        [SerializeField] Image nextBtn;
        [SerializeField] Image prevBtn;

        int currentTipIndex = 0;

        public void OnOpen()
        {
            guiMain.SetActive(true);

            currentTipIndex = 0;
            SetTipContent(tipData.tipList[currentTipIndex]);
            prevBtn.gameObject.SetActive(false);
            nextBtn.gameObject.SetActive(true);

            Time.timeScale = 0f;
        }

        public void OnClose()
        {
            guiMain.SetActive(false);
            Time.timeScale = 1f;
        }

        public void OnNext()
        {
            currentTipIndex++;
            SetTipContent(tipData.tipList[currentTipIndex]);
            prevBtn.gameObject.SetActive(true);
            
            if (currentTipIndex == tipData.tipList.Count - 1) 
                nextBtn.gameObject.SetActive(false);
        }

        public void OnPrevious()
        {
            currentTipIndex--;
            SetTipContent(tipData.tipList[currentTipIndex]);
            nextBtn.gameObject.SetActive(true);

            if (currentTipIndex == 0) 
                prevBtn.gameObject.SetActive(false);
        }

        void SetTipContent(TipItem tip)
        {
            titleText.text = tip.tipTitle;
            contentText.text = tip.tipContent;
            tipImg.sprite = tip.tipImage;
        }
    }
}

