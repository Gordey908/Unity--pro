using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    [SerializeField]
    private Color hoverColor;
    [SerializeField]
    private Color defaultColor;

    [SerializeField]
    private GameObject turretPrefab;


    private GameObject selectedNode;

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.tag == "Node")
            {
                selectedNode = hit.collider.gameObject;
                selectedNode.GetComponent<MeshRenderer>().material.color = hoverColor;
            }
            else 
            {
                if(selectedNode != null)
                {
                    selectedNode.GetComponent<MeshRenderer>().material.color = defaultColor;
                }

            }
        }
        if(Input.GetMouseButtonDown(0))
        {
            selectedNode.GetComponent<BuildSettings>().StartBuild(turretPrefab, 0.35f);
        }
    }
}
