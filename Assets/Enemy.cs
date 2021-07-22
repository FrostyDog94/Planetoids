using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public float health;
    public float maxHealth = 100;
    public float speed = 3;
    float sSpeed;
    public Transform player;
    Animator animator;
    public GameObject blood;
    bool isAlive;
    public float attackTime = 2;
    float attackTimer;
    public Transform nearestTarget;
    Collider planetCollider;
    PlanetManager planet;
    public GameObject nest;
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    float screechTime = 3;
    float screechTimer;
    bool addScore;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        isAlive = true;
        sSpeed = speed;
        screechTimer = Random.Range(1, 4);
        addScore = false;
     
    }

    // Update is called once per frame
    void Update()
    {
        if (planetCollider != null)
        { if (planetCollider.GetComponentInParent<PlanetManager>())
            {
                nearestTarget = FindNearestTarget(planetCollider.GetComponentInParent<PlanetManager>().objectsOnPlanet);
            }
        }

        Wander();

        if (health <= 0)
        {
            Die();
        }
        attackTimer -= Time.deltaTime;

       

    }

    private void OnTriggerEnter(Collider other)
    {
        planetCollider = other;

        if (other.GetComponentInParent<PlanetManager>())
        {
            planet = other.GetComponentInParent<PlanetManager>();
            if (!planet.enemiesOnPlanet.Contains(transform))
            {
                planet.enemiesOnPlanet.Add(transform);
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<PlanetManager>())
        {
            planet = other.GetComponentInParent<PlanetManager>();
            if (planet.enemiesOnPlanet.Contains(transform))
            {
                planet.enemiesOnPlanet.Remove(transform);
            }
        }
    }

    void Chase()
    {
        screechTimer -= Time.deltaTime;
        PlayRandomNoise();
        var targetDis = Vector3.Distance(transform.position, nearestTarget.position);
        var targetDir = (transform.position - nearestTarget.position).normalized;

        transform.position = Vector3.MoveTowards(transform.position, nearestTarget.position, sSpeed * Time.deltaTime);
        if (isAlive)
        {
            transform.rotation = Quaternion.FromToRotation(-transform.forward, targetDir) * transform.rotation;
        }
        else
        {
            transform.rotation = transform.rotation;
        }
        

        if (targetDis > 1)
        {
            sSpeed = speed;
            animator.SetBool("isRunning", true);
            animator.SetBool("isAttacking", false);

        }
        else
        {
            sSpeed = 0;
            if (isAlive)
            {
                Attack();
                
            }
        }
    }

    void Die()
    {
        if (addScore == false)
        {
            ScoreManager.Instance.minerals += 5;
            addScore = true;
        }
        
        planet.enemiesOnPlanet.Remove(transform);
        isAlive = false;
        sSpeed = 0;
        animator.SetBool("isDead", true);
        animator.SetBool("isRunning", false);
        
        Destroy(gameObject, 5);
        
    }

    public void Damage()
    {
        var bloodObj = Instantiate(blood, transform.position, transform.rotation);
        bloodObj.transform.parent = transform;
        health -= 50;
        Destroy(bloodObj, 5);

    }

    void Attack()
    {
        if (attackTimer <= 0)
        {
            player.GetComponent<Player>().Damage();
            animator.SetBool("isAttacking", true);
            attackTimer = attackTime;
        }
    }

Transform FindNearestTarget (List<Transform> objectsOnPlanet)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in objectsOnPlanet)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }

    void Wander()
    {
        animator.SetBool("isRunning", true);
        animator.SetBool("isAttacking", false);

        if (nearestTarget != null)
        {
            Chase();
        }
        else
        {
            transform.Translate(Vector3.forward * sSpeed * Time.deltaTime);
        }



    }

    public void GenerateNest()
    {
      
            var newNest = Instantiate(nest, transform.position, transform.rotation);
            Destroy(gameObject);
            planet.enemiesOnPlanet.Remove(transform);
            
        
 
    }

    void PlayRandomNoise()
    {
        if (screechTimer <= 0 && isAlive)
        {
            audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
            audioSource.Play();

            screechTimer = Random.Range(1, 4);
        }
    }
}
