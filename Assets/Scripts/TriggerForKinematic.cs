using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerForKinematic : MonoBehaviour
{
    public Rigidbody[] prims;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            foreach (Rigidbody rigidbody in prims)
            {
                rigidbody.isKinematic = false;
            }
        }
    }
}
