using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private EnemyData enemyData;
    private Vector3 FinishPoint;
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private GameObject boomFXPrefab;

    private uint health;
    void Awake()
    {
        FinishPoint = GameObject.FindGameObjectWithTag("Finish").transform.position;
    }
    private void Start()
    {
        agent.destination = FinishPoint;
        agent.speed = enemyData.speed;
        health = enemyData.maxHealth;
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Finish")
        {
            Destroy(gameObject);
        }
    }

    public void GetDamage()
    {
        Instantiate(boomFXPrefab, transform.position, Quaternion.Euler(-90f, 0f, 0f));
        Destroy(gameObject);
    }
}
