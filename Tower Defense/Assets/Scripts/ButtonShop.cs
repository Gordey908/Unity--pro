using UnityEngine;
using UnityEngine.UI;

public class ButtonShop : MonoBehaviour
{
    [SerializeField]
    private Text costText;
    [SerializeField]
    private Text countText;
    [SerializeField]
    private TurretData turretData;
    [SerializeField]
    private Image image;
    [SerializeField]
    private Button button;

    private int turretCount;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        var buildManager = BuildManager.Instance;
        turretCount = turretData.maxCount;

        costText.text = turretData.cost.ToString();
        countText.text = $"{turretCount}/{turretData.maxCount}";
        image.sprite = turretData.icon;

        buildManager.onBuild += ChangeCount;

        button.onClick.AddListener(() => buildManager.SetBuildTurret(turretData));
    }

    public void ChangeCount()
    {
        turretCount--;
        countText.text = $"{turretCount}/{turretData.maxCount}";

        if (turretCount == 0)
        {
            button.interactable = false;
        }
    }
}
