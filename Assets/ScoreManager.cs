using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public float minerals;
    public TMP_Text text;
    public float health;
    public TMP_Text healthValue;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        minerals = 0;
        
    }

    private void Update()
    {
        text.text = minerals.ToString();
        healthValue.text = health.ToString();
    }


}
