using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodEffect : MonoBehaviour
{
    public float radius = 5.0F;
    public float power = 10.0F;
    public float lift = 30;
    public float speed = 10;
    public bool explode = false;
    
    void FixedUpdate()
    {
        if (explode)
        {
            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
            foreach (Collider hit in colliders)
            {
                if (hit.GetComponent<Rigidbody>())
                {
                    hit.GetComponent<Rigidbody>().AddExplosionForce(power, explosionPos, radius, lift);
                }
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            explode = true;
            
            //Destroy(this, 0);
        }
       
        
    }
}