using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lam.DefenderBuilder.Enemies
{
    public class WaveManager : MonoBehaviour
    {
        [SerializeField] float timeToPrepare = 5f;
        [SerializeField] float timeforNextWave = 5f;
        private void Start()
        {
            StartCoroutine(StartSpawnCoroutine());
        }

        IEnumerator StartSpawnCoroutine()
        {
            yield return new WaitForSeconds(timeToPrepare);

            foreach (Transform child in transform)
            {
                var enemySpawner = child.GetComponent<EnemyPool>();
                enemySpawner.SetSpawnActive(true);
                StartCoroutine(enemySpawner.SpawnEnemy());
                
                yield return new WaitForSeconds(timeforNextWave);
                //enemySpawner.SetSpawnActive(false);
            }
        }
    }
}

