using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaquinaPagamento : MonoBehaviour
{
    [Header("GeneralStuff")]
    [SerializeField] Animator Porta;
    [SerializeField] MaquinaExitTrigger exit;

    private void Update()
    {
        if(exit.canClose)
        {
            Porta.SetTrigger("CanClose");
            exit.canClose = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.transform.tag == "Car" && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("TA CA");
            Porta.SetTrigger("CanOpen");
        }
    }
}
