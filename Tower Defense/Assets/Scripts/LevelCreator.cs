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
    private GameObject planePrefab;
    [SerializeField]
    private Transform nodeParents;

    void Start()
    {
        
    }

    [ContextMenu("CreateMap")]
    private void CreateMap()
    {
        while (nodeParents.childCount > 0)
        {
            DestroyImmediate(nodeParents.GetChild(0).gameObject);
        }
        GameObject plane = Instantiate(planePrefab, new Vector3((NODE_GRID_ROW_COUNT * offset) / 2 - 1, 0, (NODE_GRID_COLUMN_COUNT * offset) / 2 - 1), Quaternion.identity, nodeParents);
        plane.transform.localScale = new Vector3(0.2f * NODE_GRID_ROW_COUNT, plane.transform.localScale.y, 0.2f * NODE_GRID_COLUMN_COUNT);
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
