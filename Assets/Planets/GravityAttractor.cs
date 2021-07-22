using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAttractor : MonoBehaviour
{
    public float gravity;
    public PlanetManager planetManager;

    private void Start()
    {
        gravity = GetComponent<PlanetManager>().gravity;
    }
    public void Attract(Transform body)
    {
        Vector3 targetDir = (body.position - transform.position).normalized;
        Vector3 bodyUp = body.up;

        body.rotation = Quaternion.FromToRotation(bodyUp, targetDir) * body.rotation;

        if (body.GetComponent<Rigidbody>())
        {
            body.GetComponent<Rigidbody>().AddForce(targetDir * gravity);
        }

    }

    public void NormalAttract(Transform body)
    {
        body.GetComponent<PlayerManager>().rb.AddForce(transform.up * gravity);
    }



}
