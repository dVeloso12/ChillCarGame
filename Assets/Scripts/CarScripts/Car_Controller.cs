using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Controller : MonoBehaviour
{
    public Rigidbody sphereRB;
    private float moveInput;
    private float turnInput;

    public float fwdSpeed;
    public float backSpeed;
    public float turnSpeed;

    private void Start()
    {
        sphereRB.transform.parent = null;
    }
    private void Update()
    {
        moveInput = Input.GetAxisRaw("Vertical");
        turnInput = Input.GetAxisRaw("Horizontal");
        
        moveInput *= moveInput > 0 ? fwdSpeed : backSpeed;

    
        transform.position = sphereRB.transform.position;

        float newRotation = turnInput * turnSpeed * Time.deltaTime * Input.GetAxisRaw("Vertical");

        transform.Rotate(0, newRotation, 0,Space.World);
    }

    private void FixedUpdate()
    {
        sphereRB.AddForce(transform.forward * moveInput,ForceMode.Acceleration);
    }
}
