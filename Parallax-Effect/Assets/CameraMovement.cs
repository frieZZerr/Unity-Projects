using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //  Defines how fast the camera is moving
    public float cameraSpeed;

    void FixedUpdate()
    {
        //  Updating camera position
        Vector3 temp = new Vector3(transform.position.x+(cameraSpeed/100),transform.position.y,transform.position.z);
        transform.position = temp;
    }
}
