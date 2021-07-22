using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandOnPlanet : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Planet")
        {
            StateChanges.Instance.EnablePlayer();
            GameInfo.Instance.currentPlanet = collision.transform;
            player.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}
