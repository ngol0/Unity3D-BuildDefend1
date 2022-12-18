using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lam.DefenderBuilder
{
    public class SceneLoader : MonoBehaviour
    {
        int currentSceneIndex;
        private void Start()
        {
            currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        }

        public void LoadMain()
        {
            int nextIndex = currentSceneIndex + 1;
            SceneManager.LoadScene(nextIndex);
        }
    }
}

