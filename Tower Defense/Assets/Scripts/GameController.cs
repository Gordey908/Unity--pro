using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject nodePrefab;

    private const int NODE_GRID_ROW_COUNT = 4;

    void Start()
    {
        CreateNodes();
    }
    private void CreateNodes()
    {
        for(int x = 0; x < NODE_GRID_ROW_COUNT; x++)
        {
            for(int y = 0; y < NODE_GRID_ROW_COUNT; y++ )
            {
                Instantiate(nodePrefab, new Vector3(x, 0, y), Quaternion.identity);
            }
        }
    }
}
