using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherFire : MonoBehaviour
{
    public float force = 1000;
    public GameObject ammo;
    public Transform fireTarget;
    public Transform cam;

    public LauncherManager launcherManager;
    

    public Transform exit;

    public LayerMask atmo;

    public Transform formerTarget;

    

    // Start is called before the first frame update
    void Start()
    {
        launcherManager = GetComponent<LauncherManager>();
        OffScreenIndicator.Instance.mainCamera = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(fireTarget.transform.position, fireTarget.transform.TransformDirection(Vector3.forward), Color.green);

        if (Input.GetKeyDown(KeyCode.E))
         {
            StateChanges.Instance.EnablePlayer();
            StateChanges.Instance.DisableLauncher();           
            StateChanges.Instance.player.transform.position = exit.position;
            StateChanges.Instance.player.transform.parent = transform.parent;
         }
        
        //Launch Ship
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Destroy(gameObject);
            StateChanges.Instance.DisableLauncher();
            RaycastHit hit;
            Physics.Raycast(fireTarget.transform.position, fireTarget.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, atmo);
            var ammoObj = Instantiate(ammo, fireTarget.position, fireTarget.localRotation);
            GameInfo.Instance.targetPlanet.GetComponent<Target>().enabled = false;
            


        }

        //Select Target Planet
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            RaycastHit hit;
            Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, atmo);
            formerTarget = GameInfo.Instance.targetPlanet;
            GameInfo.Instance.targetPlanet = hit.transform;
          
           GameInfo.Instance.targetPlanet.GetComponent<Target>().enabled = true;
            
            if (formerTarget != GameInfo.Instance.targetPlanet)
            {
                formerTarget.GetComponent<Target>().enabled = false;
            }

            
        }

    }




}
