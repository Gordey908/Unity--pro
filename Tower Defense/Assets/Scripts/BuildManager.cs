using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;

    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private Color hoverColor;
    [SerializeField]
    private Color defaultColor;

    [SerializeField]
    private GameObject turretPrefab;

    private bool canBuild = false;
    private int turretIndex;
    private int cost;
    private GameObject selectedNode;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            if (hit.collider.CompareTag("Node") && canBuild)
            {
                var node = hit.collider.GetComponent<BuildSettings>();
                if (node.structure == null)
                {
                    if (selectedNode != hit.collider.gameObject)
                    {
                        ResetNodeColor();
                        selectedNode = hit.collider.gameObject;
                        selectedNode.GetComponent<MeshRenderer>().material.color = hoverColor;
                    }
                }
            }
            else
            {
                ResetNodeColor();
            }
        }

        if (Input.GetMouseButtonDown(0) && selectedNode != null)
        {
            var nodeSettings = selectedNode.GetComponent<BuildSettings>();
            if (nodeSettings.structure == null)
            {
                nodeSettings.StartBuild(turretPrefab, 0.35f, cost, turretIndex);
                InterstitialAd.Instance.TowerWasBuild();
                ResetNodeColor();
                canBuild = false;
            }
        }
    }

    public void SetBuildTurret(int cost, int buildIndex)
    {
        canBuild = true;
        turretIndex = buildIndex;
        this.cost = cost;
    }

    private void ResetNodeColor()
    {
        if (selectedNode != null)
        {
            selectedNode.GetComponent<MeshRenderer>().material.color = defaultColor;
            selectedNode = null;
        }
    }
}
