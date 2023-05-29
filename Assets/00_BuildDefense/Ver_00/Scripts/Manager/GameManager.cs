using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Lam.DefenderBuilder.UI;

namespace Lam.DefenderBuilder
{
    public class GameManager : SingletonMono<GameManager>
    {
        [Header("UI")]
        [SerializeField] TextMeshProUGUI scoreText;
        [SerializeField] GameOverMenu gameOverPopUp;

        [Header("Stats")]
        [SerializeField] int scoreToWin = 5000;

        int totalScore = 0;
        int scoreToAdd = 10;

        private void Start() {
            UpdateScoreUI();
        }

        public void OnScore()
        {
            totalScore += scoreToAdd;
            UpdateScoreUI();

            if (totalScore >= scoreToWin) OnGameOver(true);
        }

        private void UpdateScoreUI()
        {
            if (scoreText == null) return;
            scoreText.text = "Score: " + totalScore.ToString();
        }

        public void OnGameOver(bool isWin)
        {
            gameOverPopUp.OnDisplay(isWin);
        }
    }
}

