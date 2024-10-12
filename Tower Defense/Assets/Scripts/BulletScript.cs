using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;

    private float speed = 2f;

    private Transform target;

    private void Update()
    {
        
    }

    // Update is called once per frame
    private void Awake()
    {
        
    }

    public void SetTarger(Transform newTarget)
    {
        target = newTarget;
        TakeForce(target);
    }
    private void TakeForce(Transform target)
    {
        Vector3 dir = target.position - transform.position;
        rb.AddForce(dir * speed, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
