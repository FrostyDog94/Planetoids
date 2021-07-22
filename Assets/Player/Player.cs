using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxHealth = 100;
    float health;

    
    void Start()
    {
        health = maxHealth;
        
    }
    void Update()
    {
        ScoreManager.Instance.health = health;
    }

    public void Damage()
    {
        health -= 50;
    }
}
