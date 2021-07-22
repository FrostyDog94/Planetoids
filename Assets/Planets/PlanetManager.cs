using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    public Orbit orbit;
    public GravityAttractor gravityAttractor;

    public float gravity = -10;

    public float orbitRadius = 700;
    public float orbitSpeed = 0.01f;

    public float startPos;

    public Transform orbitPoint;

    public int enemyCapacity = 20;

    public int nestCapacity = 5;

    public GameObject egg;
    

    [SerializeField]
    public List<Transform> objectsOnPlanet;
    public List<Transform> enemiesOnPlanet;
    public List<Transform> nestsOnPlanet;

    public float generateNestTime = 10;
    [SerializeField]
    float generateNestTimer;

    public float launchEggTime = 60;
    [SerializeField]
    float launchEggTimer;

    List<Transform> planets;

    [SerializeField]
    Transform nestTarget;


    private void Awake()
    {
        
        orbit = GetComponent<Orbit>();
        gravityAttractor = GetComponent<GravityAttractor>();
        objectsOnPlanet = new List<Transform>();
        generateNestTimer = generateNestTime;
        launchEggTimer = launchEggTime;

        


    }
    private void Start()
    {
        GameInfo.Instance.planets.Add(transform);
        planets = GameInfo.Instance.planets;
    }

    private void Update()
    {
        if (nestsOnPlanet.Count < nestCapacity)
        {
            if (enemiesOnPlanet.Count > 0)
            {
                if (objectsOnPlanet.Count <= 0)
                {
                    generateNestTimer -= Time.deltaTime;

                }
            }
        }

        if (nestsOnPlanet.Count >= nestCapacity)
        {
            if (objectsOnPlanet.Count == 0)
            {
                launchEggTimer -= Time.deltaTime;

            }
        }

        foreach (Transform obj in objectsOnPlanet)
        {
            if (obj == null)
            {
                objectsOnPlanet.Remove(obj);
            }
        }

        foreach (Transform obj in enemiesOnPlanet)
        {
            if (obj == null)
            {
                enemiesOnPlanet.Remove(obj);
            }
        }

        foreach (Transform obj in nestsOnPlanet)
        {
            if (obj == null)
            {
                nestsOnPlanet.Remove(obj);
            }
        }

        if (generateNestTimer <= 0)
        {

            GenerateNest();
            generateNestTimer = generateNestTime;

        }

    
            
      if (nestsOnPlanet.Count >= nestCapacity && launchEggTimer <= 0) { 
            NestTargetPlanet();
            launchEggTimer = launchEggTime;
        }

        if (nestTarget != null)
        {
            if (nestTarget.GetComponent<PlanetManager>().enemiesOnPlanet.Count >= 1)
            {
                NestTargetPlanet();
            }
        }

    }


    void GenerateNest()
    {
        if (enemiesOnPlanet.Count >= 1)
        {
            enemiesOnPlanet[0].GetComponent<Enemy>().GenerateNest();
        }
    }

    void NestTargetPlanet()
    {
        for (int i = 0; i < planets.Count;)
        {
            var manager = planets[i].GetComponent<PlanetManager>();
            var dir = (planets[i].transform.position - transform.position).normalized;
            RaycastHit hit;

            Physics.Raycast(transform.position, dir, out hit, Mathf.Infinity);

            if (planets[i].transform != transform && manager != null && hit.transform.tag != "Sun" &&  manager.enemiesOnPlanet.Count == 0)
                {
               
                    nestTarget = planets[i].transform;
                    ChooseNestInSight();
                    break;
                }
                else
                {
                    nestTarget = null;
                    i++;
                }

            
        }
        
    }

    void ChooseNestInSight()
    {
        for (int i = 0; i < nestsOnPlanet.Count;)
        {
            var dir =  (nestTarget.position - nestsOnPlanet[i].GetComponent<Nest>().spawnTransform.position).normalized;

            RaycastHit hit;
            Physics.Raycast(nestsOnPlanet[i].GetComponent<Nest>().spawnTransform.position, dir, out hit, Mathf.Infinity);
            

            if (nestTarget != null && hit.transform != null)
            { 
                        if (hit.transform.root.transform == nestTarget)
                        {
                            nestsOnPlanet[i].GetComponent<Nest>().LaunchEgg(nestTarget);
                            break;
                        }
                        else
                        {
                            i++;
                        }

                
            }
         


        }
    }




}
