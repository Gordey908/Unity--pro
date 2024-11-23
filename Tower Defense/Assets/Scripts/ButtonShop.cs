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

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        var buildManager = BuildManager.Instance;
        button.onClick.AddListener(() =>
        {
            if (turretCount > 0)
            {
                buildManager.SetBuildTurret(turretData);
                ChangeCount();
            }
            else
            {
                Debug.Log("No turrets available to build!");
            }
        });
    }

    private void Init()
    {
        turretCount = turretData.maxCount;
        costText.text = turretData.cost.ToString();
        countText.text = $"{turretCount}/{turretData.maxCount}";
        image.sprite = turretData.icon;
    }

    public void ChangeCount()
    {

        turretCount--;
        countText.text = $"{turretCount}/{turretData.maxCount}";
    }
}
