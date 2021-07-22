using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateLauncher : MonoBehaviour
{
    public GameObject cam;
    public PlayerManager playerManager;

    

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            Physics.Raycast(cam.transform.position, transform.TransformDirection(Vector3.forward), out hit, 10);
            if(hit.transform.tag == "Launcher")
            {
                StateChanges.Instance.launcherCam = cam;
                StateChanges.Instance.launcherManager = hit.transform.GetComponent<LauncherManager>();
                StateChanges.Instance.EnableLauncher(hit);
                StateChanges.Instance.DisablePlayer();
                
            }
        }
    }


}
