using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaquinaExitTrigger : MonoBehaviour
{
    [HideInInspector]
    public bool canClose;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Car")
        {
            canClose = true;
        }

    }
}
