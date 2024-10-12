using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;
    [SerializeField]
    private float range = 1f;

    [Header("Turret settings")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField]
    private Transform[] gunBarrel;
    [SerializeField]
    private float rechargeTime;
    private int currentBurrelIndex = 0;

    private List<Transform> targetsInRange = new List<Transform>();

    private bool isShooting = false;  // Флаг, чтобы избежать многократного запуска корутины

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (targetsInRange != null && targetsInRange.Count > 0)
        {
            target = FindTarget();  // Найти ближайшую цель
            if (target != null && !isShooting)
            {
                transform.LookAt(target);
                StartCoroutine(Shoot());  // Начать стрельбу, если есть цель и не стреляем
            }
        }
    }

    private Transform FindTarget()
    {
        if (targetsInRange == null || targetsInRange.Count == 0) return null;

        RemoveNullObjects();

        Transform newTarget = targetsInRange.FirstOrDefault();

        foreach (Transform t in targetsInRange)
        {
            if (t == null)
            {
                targetsInRange.Remove(t);
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
        targetsInRange.RemoveAll(t => t == null);
    }

    private IEnumerator Shoot()
    {
        while (target != null)
        {
            yield return new WaitForSeconds(rechargeTime);

            GameObject bullet = Instantiate(bulletPrefab, gunBarrel[currentBurrelIndex].position, gunBarrel[currentBurrelIndex].rotation);
            bullet.transform.parent = null;

            BulletScript bulletScript = bullet.GetComponent<BulletScript>();
            bulletScript.SetTarger(target);
            currentBurrelIndex++;
            if (currentBurrelIndex == gunBarrel.Length)
            {
                currentBurrelIndex = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Enemy"))
        {
            targetsInRange.Add(col.transform);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Enemy"))
        {
            targetsInRange.Remove(col.transform);
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Enemy"))
        {
            target = FindTarget();
        }
    }
}
