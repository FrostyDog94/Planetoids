using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherGravity : MonoBehaviour
{
    GravityAttractor planet;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (planet != null)
        {
            planet.Attract(transform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 targetDir = (transform.position - other.transform.position).normalized;

        if (other.GetComponentInParent<GravityAttractor>())
        {
            planet = other.GetComponentInParent<GravityAttractor>();

            transform.parent = other.transform.parent;

        }
    }
}
