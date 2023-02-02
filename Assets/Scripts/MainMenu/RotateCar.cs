using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCar : MonoBehaviour
{

    [SerializeField] float rotateSpeed = 1f;
    public bool canRotate;

    private void OnMouseDrag()
    {
        if(canRotate)
        {
            float x = Input.GetAxis("Mouse X") * rotateSpeed;

            transform.Rotate(Vector3.down, x);

        }
    }

}
