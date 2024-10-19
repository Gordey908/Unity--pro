using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSettings : MonoBehaviour
{
    public GameObject structure { get; private set; }

    public void StartBuild(GameObject structurePrefab, float high)
    {
        if(structure == null)
        {
            Vector3 pos = new Vector3(transform.position.x, transform.position.y + high, transform.position.z);
            var newStructure = Instantiate(structurePrefab, pos, Quaternion.identity);
        }
    }
}
