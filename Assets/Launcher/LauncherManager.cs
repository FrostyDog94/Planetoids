using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherManager : MonoBehaviour
{
    public Camera cam;
    public Controller_Launcher controller;
    public LauncherFire launcherFire;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<Controller_Launcher>();
        launcherFire = GetComponent<LauncherFire>();
        player = GameObject.FindGameObjectWithTag("Player");

    }




}
