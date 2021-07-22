using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravityBody : MonoBehaviour
{
    GravityAttractor planet;
    Rigidbody rb;
    
    public bool isOnPlanet;

    public PlayerManager playerManager;

    private void Awake()
    {
        rb = playerManager.rb;
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        
        isOnPlanet = true;
    }

    private void FixedUpdate()
    {
        if (isOnPlanet == true)
        {
            planet.Attract(transform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Vector3 targetDir = (transform.position - other.transform.position).normalized;

        if (other.GetComponentInParent<GravityAttractor>())
        {
            planet = other.GetComponentInParent<GravityAttractor>();
            isOnPlanet = true;
            transform.parent = other.transform.parent;

        }
        else
        {
            planet = null;
            isOnPlanet = false;
        }

        //other.GetComponentInParent<ObjectsOnPlanet>().objOnPlanet.Add(gameObject);
        
    }
    private void OnTriggerExit(Collider other)
    {
        TakeOff();
    }

    void TakeOff()
    {
        planet = null;
        transform.parent = null;
        isOnPlanet = false;

    }
}
