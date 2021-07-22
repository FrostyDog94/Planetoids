using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    public FPSController controller;
    public GravityBody gravityBody;
    public Rigidbody rb;
    public Camera cam;

   
    


    // Start is called before the first frame update
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

        rb = GetComponent<Rigidbody>();
        controller = GetComponent<FPSController>();
        gravityBody = GetComponent<GravityBody>();
        cam = GetComponentInChildren<Camera>();
        gravityBody.enabled = true;
    }





}
