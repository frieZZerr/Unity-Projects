using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public GameObject mainCamera;

    //  Defines how much of the
    //      effect is to be applied...
    //  - 0 --> biggest effect
    //  - 1 --> no effect
    public float parallax;

    float length, startPos;

    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        float temp = mainCamera.transform.position.x * (1 - parallax);
        float distance = mainCamera.transform.position.x * parallax;

        //  Setting position
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        //  Updating bounds
        if (temp > startPos + length)
            startPos += length;
        if (temp < startPos-length)
            startPos -= length;
    }
}
