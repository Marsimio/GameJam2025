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
    }
    
    //Configs are set in a way where you can just create a new entry to their list each customizable independantly
    
    //Joe customize the values on each enemy config I am too tired for this shit rn

    public List<EnemyConfig> enemyConfigs;

    private List<Coroutine> enemyCoroutines = new List<Coroutine>();

    private void Start()
    {
        foreach (var config in enemyConfigs)
        {
            SpawnEnemy(config);
        }
    }

    private void SpawnEnemy(EnemyConfig config)
    {
        enemyCoroutines.Add(StartCoroutine(SpawnContinously(config)));
    }

    IEnumerator SpawnContinously(EnemyConfig config)
    {
        while (true)
        {
            float spawnHeight = Random.Range(config.spawnHeightMin, config.spawnHeightMax);
            Vector3 spawnPosition = new Vector3(transform.position.x, spawnHeight, 0);
            GameObject myEnemyPrefab = config.enemyPool.GetPooledObject();
            if (myEnemyPrefab)
            {
                myEnemyPrefab.transform.position = spawnPosition;
                myEnemyPrefab.SetActive(true);
            }
            yield return new WaitForSeconds(config.spawnInterval);
        }
    }

    private void OnDestroy()
    {
        foreach (var coroutine in enemyCoroutines)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
        }
        enemyCoroutines.Clear();
    }
}