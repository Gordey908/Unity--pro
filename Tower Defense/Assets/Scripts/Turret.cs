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
    public float turnSpeed = 1f;
    [SerializeField]
    private AudioSource audioSource;

    private List<Transform> targetsInRange = new List<Transform>();

    private bool isShooting = false;  // Флаг, чтобы избежать многократного запуска корутины

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private void Start()
    {
        StartCoroutine(Shoot());
    }

    private void Update()
    {
        LookAtTarget();
    }
    private void LookAtTarget()
    {
        if (target != null)
        {
            Quaternion look = Quaternion.LookRotation(target.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, look, turnSpeed);
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
        while (true)
        {
            yield return new WaitUntil(() => target != null);
            if (isTargetLocked())
            {
                GameObject bullet = Instantiate(bulletPrefab, gunBarrel[currentBurrelIndex].position, gunBarrel[currentBurrelIndex].rotation);
                bullet.transform.parent = null;

                audioSource.pitch = Random.Range(0.9f, 1.2f);
                audioSource.Play();

                BulletScript bulletScript = bullet.GetComponent<BulletScript>();
                bulletScript.SetTarget(target);

                currentBurrelIndex++;
                if (currentBurrelIndex == gunBarrel.Length)
                {
                    currentBurrelIndex = 0;
                }
            }

            yield return new WaitForSeconds(rechargeTime);
        }
    }

    private bool isTargetLocked()
    {
        float angle = Quaternion.Angle(transform.rotation, target.rotation);
        if (angle > 60 && angle < 120)
        {
            return true;
        }
        return false;
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
