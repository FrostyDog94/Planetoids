using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    public GameObject selectedObject;
    public ObjectMenu objectMenu;
    public LayerMask atmo;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (objectMenu.menuOpen == false)
            {
                PlaceObject();
            }
        }
    }

    void PlaceObject()
    {
        GameObject placedObject;
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, ~atmo))
        {
            placedObject = Instantiate(selectedObject, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }
}
