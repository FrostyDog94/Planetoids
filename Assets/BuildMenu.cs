using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenu : MonoBehaviour
{
    public BuildMenuHolder buildMenuHolder;
    public CreateLauncher createLauncher;
    public float buildCost;

    public GameObject menu;

    public bool isOpen;

    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            EnableMenu();
        }
    }

    public void ChooseShuttle()
    {
        createLauncher.selectedObject = buildMenuHolder.shuttle;
        buildCost = 50;
        createLauncher.buildCost = buildCost;
    }

    public void ChooseOther()
    {
        createLauncher.selectedObject = buildMenuHolder.other;
        buildCost = 0;
        createLauncher.buildCost = buildCost;
    }

    void EnableMenu()
    {
        if (isOpen)
        {
            menu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isOpen = false;
        }
        else
        {
            menu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isOpen = true;
        }
    }
}
