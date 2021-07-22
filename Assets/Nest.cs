using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Nest : MonoBehaviour, IDamageable
{
    public float spawnTime;
    float spawnTimer;
    public Transform spawnTransform;
    public GameObject enemy;
    PlanetManager planetManager;
    public int capacity;
    public bool atCapacity;
    public float maxHealth = 100;
    float health;
    public GameObject particle;
    List<Transform> planets;
    public GameObject egg;
    public bool isActive = true;
    public float launchTime;
    float launchTimer;
    public Transform hatchSpawn1, hatchSpawn2, hatchSpawn3, hatchSpawn4;

    // Start is called before the first frame update
    void Start()
    {
        launchTime = Random.Range(10, 20);
        spawnTimer = spawnTime;
        launchTimer = launchTime;
        atCapacity = false;
        health = maxHealth;
        planets = GameInfo.Instance.planets;

    }

    // Update is called once per frame
    void Update()
    {
        launchTimer -= Time.deltaTime;

        if (planetManager != null)
        {
            capacity = planetManager.enemyCapacity;
        }
        if (planetManager != null)
        {
            CheckCapacity();
        }
        if (atCapacity == false)
        {
            Spawn();
        }
        Spin();

        if (health <= 0)
        {
            Die();
        }
/*
        if (launchTimer<= 0)
        {
            if (isActive)
            {
                SelectTargetPlanet();
                launchTimer = launchTime;
                
            }
        }
*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<PlanetManager>())
        {
            planetManager = other.GetComponentInParent<PlanetManager>();
            if (!planetManager.nestsOnPlanet.Contains(transform))
            {
                planetManager.nestsOnPlanet.Add(transform);
            }
            

        }
    }

    

    void Spin()
    {
        spawnTransform.transform.Rotate(new Vector3(0, 100, 0), 100 * Time.deltaTime);
    }

    void Spawn()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            var obj = Instantiate(enemy, spawnTransform.position, spawnTransform.rotation);
            obj.GetComponent<Rigidbody>().AddForce(spawnTransform.transform.forward * 200, ForceMode.Impulse);
            spawnTimer = spawnTime;
        }
    }

    bool CheckCapacity()
    {
        if (planetManager.enemiesOnPlanet.Count >= capacity)
        {
            atCapacity = true;
        } else
        {
            atCapacity = false;
        }

        return atCapacity;
    }

    public void Damage()
    {
        var mParticle = Instantiate(particle, transform.position, transform.rotation);
        mParticle.transform.parent = transform;
        health -= 10;
        Destroy(mParticle, 5);
    }

    void Die()
    {
        Destroy(gameObject);
        planetManager.nestsOnPlanet.Remove(transform);
    }

    void SelectTargetPlanet()
    {
        for (int i = 0; i < planets.Count; i++)
        {
            if (planets[i].transform != planetManager.transform)
            {
                if (planets[i].GetComponent<PlanetManager>().enemiesOnPlanet.Count <= 0)
                {
                    var eggObj = Instantiate(egg, spawnTransform.position, spawnTransform.rotation);
                    eggObj.GetComponent<Egg>().targetPlanet = planets[i];

                    break;
                    /*
                    var dir = planets[i].transform.position - spawnTransform.transform.position;
                    RaycastHit hit;
                    Physics.Raycast(spawnTransform.position, dir, out hit, Mathf.Infinity);
                    if (hit.transform == planets[i].transform)
                    {

                    }
                    */

                }
            }
        }
    }

    public void LaunchEgg(Transform target)
    {
        var eggObj = Instantiate(egg, spawnTransform.position, spawnTransform.rotation);
        eggObj.GetComponent<Egg>().targetPlanet = target;
    }

    public void EggHatchSpawn()
    {
        Instantiate(enemy, hatchSpawn1.position, hatchSpawn1.rotation);
        Instantiate(enemy, hatchSpawn2.position, hatchSpawn2.rotation);
        Instantiate(enemy, hatchSpawn3.position, hatchSpawn3.rotation);
        Instantiate(enemy, hatchSpawn4.position, hatchSpawn4.rotation);
    }
}
