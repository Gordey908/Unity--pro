using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;
    [SerializeField]
    private float range = 1f;

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
    private void FindTarget()
    {
        if (targestInRange == null) return;
        float distance = 0;

        foreach (Transform t in targestInRange)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, t.position);
            if (distanceToEnemy < range)
            {
                distance = distanceToEnemy;
                target = t;
            }
        }

        if(distance > range)
        {
            target = null;
            return;
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
            FindTarget();
        }
    }
}
