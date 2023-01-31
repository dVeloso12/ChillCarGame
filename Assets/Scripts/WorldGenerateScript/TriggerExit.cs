using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerExit : MonoBehaviour
{
    public float delay = 5f;
    
    public delegate void ExitAction();
    public static event ExitAction OnChunkExited;

    private bool exited = false;

    private void OnTriggerExit(Collider other)
    {
        
        if (other.tag == "Car")
        {
            if (!exited)
            {
                exited = true;
                OnChunkExited();
                this.gameObject.SetActive(false);
            }


        }
    }

    IEnumerator WaitAndDeactivate()
    {
        yield return new WaitForSeconds(delay);

        this.gameObject.SetActive(false);


    }



}
