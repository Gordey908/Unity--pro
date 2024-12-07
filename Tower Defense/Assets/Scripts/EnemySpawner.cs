using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private float countdown = 5f;
    [SerializeField]
    private float timeBetweenSpawnWave = 1;

    private int waveNumber = 1;
    private int enemyCount = 1;
    private void Start()
    {
        //InvokeRepeating("SpawnEnemy", 0f, 0.000001f);
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        Debug.Log(waveNumber);
        yield return new WaitForSeconds(countdown);

        for (int i = 0; i < enemyCount; i++)
        {
            GameObject obj = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            obj.transform.parent = transform;
            yield return new WaitForSeconds(timeBetweenSpawnWave);
        }
        waveNumber++;
        if (enemyCount < 10)
        {
            enemyCount++;
        }
        StartCoroutine(SpawnWave());
    }
}
