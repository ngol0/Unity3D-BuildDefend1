using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lam.DefenderBuilder.Enemies
{
    public class EnemyPool : MonoBehaviour
    {
        [SerializeField] GameObject enemyPrefab;
        
        [Header("Stats")]
        [SerializeField] [Range(0, 30)] int poolSize = 5;
        [SerializeField] [Range(0.1f, 30f)] float spawnTime;

        GameObject[] enemyPool;

        private bool canSpawn;

        private void Awake() {
            PopulatePool();
        }

        private void Start() {
            //StartCoroutine(SpawnEnemy());
        }

        private void PopulatePool()
        {
            enemyPool = new GameObject[poolSize];

            for (int i = 0; i < poolSize; i++)
            {
                enemyPool[i] = Instantiate(enemyPrefab, transform);
                enemyPool[i].SetActive(false);
            }
        }

        private GameObject GetEnemy()
        {
            foreach (var enemy in enemyPool)
            {
                if (!enemy.activeInHierarchy)
                {
                    return enemy;
                }
            }
            return null;
        }

        public void SetSpawnActive(bool active)
        {
            canSpawn = active;
        }

        public IEnumerator SpawnEnemy()
        {
            while (canSpawn)
            {
                var enemy = GetEnemy();
                if (enemy) enemy.SetActive(true);
                yield return new WaitForSeconds(spawnTime);
            }
        }
    }
}

