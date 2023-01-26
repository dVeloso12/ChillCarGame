using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehiclePhysics;
using VehiclePhysics.UI;


public class UIController : MonoBehaviour
{
    [SerializeField] Car_Controller controller;
    [SerializeField] InputMonitor input;
    [SerializeField] Dashboard dash;
    [SerializeField] AidsPanel aids;
    [SerializeField] IgnitionKey igntion;

    private void Update()
    {
        UpdateCar();
    }
    void UpdateCar()
    {
        VehicleBase vb = controller.currentCar.GetComponent<VehicleBase>();
        igntion.vehicle = vb;
        input.vehicle = vb;
        dash.vehicle = vb;
        aids.vehicle = vb;
    }

}
