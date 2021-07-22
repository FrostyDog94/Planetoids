using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Launcher : MonoBehaviour
{
    public float mouseSensitivityX = 1;
    public float mouseSensitivityY = 1;

    float verticalLookRotation;

    public Transform cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Look rotation:
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivityX);
        verticalLookRotation += Input.GetAxis("Mouse Y") * mouseSensitivityY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -60, 60);
        cameraTransform.localEulerAngles = Vector3.left * verticalLookRotation;
    }
}
