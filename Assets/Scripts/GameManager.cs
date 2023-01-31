using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehiclePhysics;
using VehiclePhysics.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    [SerializeField] Dashboard dash;

    [SerializeField] bool viaVerde;

    private float currentDistanceDone;
    [SerializeField] TextMeshProUGUI currentDistaceTxt;

    private void Update()
    {
        currentDistanceDone += dash.CurrentSpeed * Time.deltaTime;
        currentDistaceTxt.text = Mathf.Round(currentDistanceDone).ToString();
        if(currentDistanceDone >= 2000)
        {
            viaVerde = true;
        }
    }

}
