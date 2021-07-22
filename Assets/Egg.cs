using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    public Transform targetPlanet;
    public float speed = 30;
    public GameObject enemy;
    public GameObject explosion;

  
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPlanet.position, speed * Time.deltaTime);
        transform.LookAt(targetPlanet.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Planet")
        {
            var enemyObj = Instantiate(enemy, transform.position, transform.rotation);
            var explosionObj = Instantiate(explosion, transform.position, GameInfo.Instance.player.transform.rotation);
            explosionObj.transform.parent = collision.transform;
            enemyObj.GetComponent<AudioSource>().Play();
            enemyObj.GetComponent<Nest>().EggHatchSpawn();
            Destroy(gameObject);
            Destroy(explosionObj, 8);
        }
    }
}
