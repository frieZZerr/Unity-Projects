using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    float xRotation = 0f;
    float mouseX, mouseY;

    public Transform playerBody;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OldControl()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        //playerBody.Rotate(Vector3.up * mouseX);
        playerBody.rotation = Quaternion.Euler(0f, mouseX, 0f);
    }
}
