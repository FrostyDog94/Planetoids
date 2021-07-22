using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchShip : MonoBehaviour
{
    public float force = 1000;
    public GameObject ammo;
    public Transform fireTarget;

    public LauncherManager launcherManager;

    public Transform exit;
    GameObject ammoObj;

    // Start is called before the first frame update
    void Start()
    {
        launcherManager = GetComponent<LauncherManager>();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var ammoObj = Instantiate(ammo, fireTarget.position, fireTarget.rotation);
            ammoObj.GetComponent<Rigidbody>().AddForce(ammoObj.transform.TransformDirection(Vector3.forward) * force);
            if (ammoObj.transform.tag == "Ship")
            {
                FireShip();
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ExitLauncher();
        }
    }

    void ExitLauncher()
    {
        fireTarget.gameObject.SetActive(false);
        launcherManager.controller.enabled = false;
        this.enabled = false;
        launcherManager.player.GetComponent<MeshRenderer>().enabled = true;
        launcherManager.player.transform.position = exit.position;
    }

    void FireShip()
    {
        fireTarget.gameObject.SetActive(false);
        launcherManager.controller.enabled = false;
        this.enabled = false;
        
    }
}
