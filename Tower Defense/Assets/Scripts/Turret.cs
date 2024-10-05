using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;
    [SerializeField]
    private float range = 1f;

    [Header("Bullet settings")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] 
    private Transform[] gunBarrel;
    [SerializeField]
    private float rechargeTime;


    private List<Transform> targestInRange = new List<Transform>();

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private void Start()
    {
        //InvokeRepeating("FindTarget", 0f, 0.3f);
    }
    private void Update()
    {
        if(targestInRange != null)
        {
            transform.LookAt(target);
        }
    }
    private Transform FindTarget()
    {
        if (targestInRange == null) return null;
        Transform newTarget = targestInRange.First();

        RemoveNullObjects();
        foreach (Transform t in targestInRange)
        {
            if(t == null)
            {
                targestInRange.Remove(t);
                continue;
            }
            float distanceToEnemy = Vector3.Distance(transform.position, t.position);
            float distanceToPrevEnemy = Vector3.Distance(transform.position, newTarget.position);
            if (distanceToEnemy < distanceToPrevEnemy)
            {
                newTarget = t;
            }
        }
        return newTarget;
    }
    private void RemoveNullObjects()
    {
        var nullObjects = new List<Transform>();
        foreach(Transform t in targestInRange)
        {
            if(t == null)
            {
                nullObjects.Add(t);
            }
        }   
        foreach(Transform t in nullObjects)
        {
            targestInRange.Remove(t);
        }    
    }
    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Enemy")
        {
            targestInRange.Add(col.transform);
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Enemy")
        {
            targestInRange.Remove(col.transform);
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if(col.tag == "Enemy")
        {
            target = FindTarget();
        }
    }
}
