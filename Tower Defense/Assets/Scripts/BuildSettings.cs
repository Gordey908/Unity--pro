using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSettings : MonoBehaviour
{
    public GameObject structure { get; private set; }
    public AudioSource audioSource;

    public void StartBuild(GameObject structurePrefab, float high, int cost, int index)
    {
        if(structure == null)
        {
            Vector3 pos = new Vector3(transform.position.x, transform.position.y + high, transform.position.z);
            structure = Instantiate(structurePrefab, pos, Quaternion.identity);
            audioSource.pitch = Random.Range(9f, 1.2f);
            audioSource.Play();
        }
    }
}
