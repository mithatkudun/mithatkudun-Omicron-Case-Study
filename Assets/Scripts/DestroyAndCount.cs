using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAndCount : MonoBehaviour
{
    public int CountGameObjects;
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="GameObjects")
        {
            Destroy(other, 1);
            CountGameObjects++;
        }
    }
}
