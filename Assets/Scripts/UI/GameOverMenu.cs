using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace Lam.DefenderBuilder.UI
{
    public class GameOverMenu : MonoBehaviour
    {
        [SerializeField] MenuTextData textData;

        [SerializeField] TextMeshProUGUI title;
        [SerializeField] TextMeshProUGUI noti;
        [SerializeField] GameObject guiMain;

        int currentSceneIndex;

        private void Start() {
            currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        }

        public void OnDisplay(bool isWin)
        {
            OnOpen();
            if (isWin)
            {
                title.text = textData.winTitle;
                noti.text = textData.winNoti;
            }
            else
            {
                title.text = textData.loseTitle;
                noti.text = textData.loseNoti;
            }

            Time.timeScale = 0f;
        }

        void OnOpen()
        {
            guiMain.SetActive(true);
        }

        void OnClose()
        {
            guiMain.SetActive(false);
        }

        public void OnReplay()
        {
            OnClose();
            SceneManager.LoadScene(currentSceneIndex);
            Time.timeScale = 1f;
        }
    }
}

