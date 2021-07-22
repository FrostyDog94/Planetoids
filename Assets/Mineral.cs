using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mineral : MonoBehaviour, IDamageable
{
    public GameObject mineParticle;
    float health;
    public float maxHealth = 30;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    public void Damage()
    {

        health -= 10;
       
    }

    void Die()
    {
        Destroy(gameObject);
        GetComponent<MeshRenderer>().enabled = false;
        ScoreManager.Instance.minerals += 10;
    }
}
