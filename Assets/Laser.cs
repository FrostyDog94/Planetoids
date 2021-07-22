using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    Transform cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }

    }

    void Shoot()
    {
        RaycastHit hit;
        Physics.Raycast(cam.position, transform.forward, out hit, 10);

        hit.transform.GetComponent<IDamageable>().Damage();
    }
}
