using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLauncher : MonoBehaviour
{
    public GameObject selectedObject;
    public Transform buildTransform;
    GameObject launcherObj;
    public float buildCost;

    

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (ScoreManager.Instance.minerals >= buildCost)
            {
                var launcherObj = Instantiate(selectedObject, buildTransform.position, buildTransform.rotation);
                ScoreManager.Instance.minerals -= buildCost;
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Planet")
        {
            launcherObj.transform.parent = other.transform.parent;
        }
    }
}
