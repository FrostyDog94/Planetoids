using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StateChanges : MonoBehaviour
{
    public static StateChanges Instance { get; private set; }

    public GameObject player;
    public GameObject ship;
   // public GameObject launcher;
    public LauncherManager launcherManager;
    public GameObject playerCam;
    public Transform exit;
    public GameObject launcherCam;
    public FPSController playerController;
    
    

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

        GameInfo.Instance.shuttleText.enabled = false;
        playerController = player.GetComponent<FPSController>();
    }


    public void EnableLauncher(RaycastHit hit)
    {
        var launchMan = hit.transform.GetComponent<LauncherManager>();
        launchMan.cam.gameObject.SetActive(true);
        launchMan.controller.enabled = true;
        launchMan.launcherFire.enabled = true;
        GameInfo.Instance.shuttleText.enabled = true;
        GameInfo.Instance.text.text = "";


    }

    public void DisablePlayer()
    {
        player.GetComponent<MeshRenderer>().enabled = false;
        player.GetComponent<CreateLauncher>().enabled = false;
        playerCam.SetActive(false);
        player.GetComponent<ActivateLauncher>().enabled = false;
        player.transform.position = transform.position;
        playerController.enabled = false;
        
        

    }

    public void DisableLauncher()
    {
        launcherManager.cam.gameObject.SetActive(false);
        launcherManager.controller.enabled = false;
        launcherManager.launcherFire.enabled = false;
        GameInfo.Instance.targetPlanet.GetComponent<Target>().enabled = false;
        GameInfo.Instance.shuttleText.enabled = false;


    }

    public void EnablePlayer()
    {
        player.GetComponent<MeshRenderer>().enabled = true;
        player.GetComponent<CreateLauncher>().enabled = true;
        playerCam.SetActive(true);
        player.GetComponent<ActivateLauncher>().enabled = true;
        playerController.enabled = true;



    }
}
