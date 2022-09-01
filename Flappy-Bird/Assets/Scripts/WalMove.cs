using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalMove : MonoBehaviour
{
    public float speed;

    void Update()
    {
        //  Moving the walls
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
}
