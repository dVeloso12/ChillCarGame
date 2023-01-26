using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehiclePhysics;

public class Car_Controller : MonoBehaviour
{

    [SerializeField] VPCameraController controller;
    [SerializeField] List<GameObject> ListOfCars;
    [SerializeField] public GameObject currentCar;
    [Header("Car Changes")]
    [SerializeField] VPStandardInput.ThrottleAndBrakeMode mode;

    private void Start()
    {
        controller.target = currentCar.transform;
        UpdateValuesCamera();
        UpdateCar();
    }

    private void Update()
    {
        UpdateCar();
    }

    void UpdateValuesCamera()
    {
        controller.smoothFollow.distance = 3.2f;
        controller.smoothFollow.height = 1.24f;

    }
    void UpdateCar()
    {
        currentCar.GetComponent<VPStandardInput>().throttleAndBrakeMode = mode;
    }

    void ChangeCar(GameObject car)
    {
        controller.target = car.transform;
        currentCar = car;
    }

}
