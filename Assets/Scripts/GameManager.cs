using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehiclePhysics;
using VehiclePhysics.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] MenuSettings data;
    [SerializeField] Dashboard dash;
    [SerializeField] Car_Controller carController;

    [SerializeField] bool viaVerde;

    private float currentDistanceDone;
    [SerializeField] TextMeshProUGUI currentDistaceTxt;

    private void Awake()
    {
        carController = GetComponent<Car_Controller>();
        getCar();
        
    }

    void getCar()
    {
        foreach(GameObject car in carController.ListOfCars)
        {
            if(car.GetComponent<CarIndex>().Index == data.carIndex)
            { 
               var spawnCar = Instantiate(car, new Vector3(-362.75f, -0.0599999987f, 180.470001f), new Quaternion(0, 0.631536365f, 0, 0.775346339f));
                carController.currentCar = spawnCar;
            }
        }
    }
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