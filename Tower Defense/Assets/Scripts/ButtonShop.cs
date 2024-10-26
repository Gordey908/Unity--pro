using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonShop : MonoBehaviour
{
    [SerializeField]
    private Text CountText;
    [SerializeField]
    private int Count;
    [SerializeField]
    private int BuildIndex;


    [SerializeField]
    private Button button;

    private void Start()
    {
        CountText.text = Count.ToString();
        //button.OnClock.AddListener(() => manager.SetBuildTurret(cost, BuildIndex));
    }

    /*private void SetBuildTurret(int.cost, int buildIndex)
    {

    }*/

}
