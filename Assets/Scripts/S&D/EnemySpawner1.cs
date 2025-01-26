using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner1 : MonoBehaviour
{
    [System.Serializable]
    public class EnemyConfig
    {
        public ObjectPooling enemyPool; 
        public float spawnInterval; 
        public float spawnHeightMax; 
        public float spawnHeightMin; 
        public int spawnCountAtTime; 
        public int maxSpawnCountAtTime; 
        public float spawnIncreaseInterval; 
    }

    public List<EnemyConfig> enemyConfigs;

    private void Start()
    {
        if (enemyConfigs == null || enemyConfigs.Count == 0)
        {
            return;
        }

        foreach (var config in enemyConfigs)
        {
            if (config.enemyPool == null)
            {
                continue;
            }

            StartCoroutine(HandleSpawnIncrease(config));
            StartCoroutine(HandleEnemySpawning(config));
        }
    }

    private IEnumerator HandleSpawnIncrease(EnemyConfig config)
    {
        while (true)
        {
            yield return new WaitForSeconds(config.spawnIncreaseInterval);

            if (config.spawnCountAtTime < config.maxSpawnCountAtTime)
            {
                config.spawnCountAtTime++;
            }
        }
    }

    private IEnumerator HandleEnemySpawning(EnemyConfig config)
    {
        yield return new WaitForSeconds(config.spawnInterval);

        while (true)
        {
            for (int i = 0; i < config.spawnCountAtTime; i++)
            {
                float spawnHeight = Random.Range(config.spawnHeightMin, config.spawnHeightMax);
                Vector3 spawnPosition = new Vector3(transform.position.x + i * 2.0f, spawnHeight, 0);

                GameObject enemy = config.enemyPool.GetPooledObject();
                if (enemy != null)
                {
                    enemy.transform.position = spawnPosition;
                    enemy.SetActive(true);
                }
            }

            yield return new WaitForSeconds(config.spawnInterval);
        }
    }
}
