using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour

{

    public Transform targetPlanet;
    public float speed = 1000;
    

    private void Start()
    {
        targetPlanet = GameInfo.Instance.targetPlanet;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPlanet.position, speed * Time.deltaTime);
        transform.LookAt(targetPlanet);
    }
}

