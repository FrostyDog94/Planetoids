using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMenu : MonoBehaviour
{
    public ObjectHolder objectHolder;
    public ObjectPlacer objectPlacer;

    public GameObject menu;

    public bool menuOpen;

    private void Start()
    {
        menuOpen = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (menuOpen)
            {
                menu.SetActive(false);
                menuOpen = false;
            }
            else
            {
                menu.SetActive(true);
                menuOpen = true;
            }
        }
    }

    public void ChooseEnemy()
    {
        objectPlacer.selectedObject = objectHolder.enemy;
    }

    public void ChooseMineral()
    {
        objectPlacer.selectedObject = objectHolder.mineral;
    }

    public void ChooseNest()
    {
        objectPlacer.selectedObject = objectHolder.nest;
    }

    public void ChoosePlayer()
    {
        objectPlacer.selectedObject = objectHolder.player;
    }

}
