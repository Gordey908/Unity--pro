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

    private TurretData currentTurretData;
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
            if (hit.collider.CompareTag("Node"))
            {
                var node = hit.collider.GetComponent<BuildSettings>();
                if (node.structure == null && currentTurretData != null)
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
            if (nodeSettings.structure == null && currentTurretData != null)
            {

                nodeSettings.StartBuild(
                    currentTurretData.turretPrefab,
                    0.35f,
                    currentTurretData.cost,
                    currentTurretData.index
                );


                InterstitialAd.Instance.TowerWasBuild();


                ResetNodeColor();
                currentTurretData = null;
            }
        }
    }

    public void SetBuildTurret(TurretData turretData)
    {
        currentTurretData = turretData;
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
