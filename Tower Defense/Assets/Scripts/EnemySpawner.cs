using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private float countdown = 3f;
    [SerializeField]
    private float timeBetweenSpawnWave = 1;

    private uint waveNumber = 1;
    private void Start()
    {
        //InvokeRepeating("SpawnEnemy", 0f, 0.000001f);
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(countdown);
        for (int i = 0; i < waveNumber; i++)
        {
            GameObject obj = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            obj.transform.parent = transform;
            yield return new WaitForSeconds(timeBetweenSpawnWave);
        }
        waveNumber++;
        StartCoroutine(SpawnWave());
    }
}
