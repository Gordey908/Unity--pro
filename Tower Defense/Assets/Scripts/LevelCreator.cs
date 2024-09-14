using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    [Header("Settings")]


    [SerializeField]
    private int NODE_GRID_ROW_COUNT = 4;

    [SerializeField]
    private int NODE_GRID_COLUMN_COUNT = 4;

    [SerializeField]
    private float offset = -0.5f;

    [Space(15)]
    [Header("Prefabs:")]
    [SerializeField]
    private GameObject nodePrefab;
    [SerializeField]
    private Transform nodeParents;

    void Start()
    {
        
    }

    [ContextMenu("CreateNodes")]
    private void CreateNodes()
    {
        //if(nodeParents.childCount > 0)
        //{
            //foreach(nodeParents child in transform.GetComponentsInChildren<Transform>())
            //{
                //DestroyImmediate(child);
            //}
        //}
        for(int x = 0; x < NODE_GRID_ROW_COUNT; x++)
        {
            for(int z = 0; z < NODE_GRID_COLUMN_COUNT; z++ )
            {
                GameObject obj = Instantiate(nodePrefab, new Vector3(x * offset, 0, z * offset), Quaternion.identity, nodeParents);
                obj.name = "Node: " + x + " " + z;
            }
        }
    }
}
