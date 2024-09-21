using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 0f, 5f);
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, transform);
    }
}
