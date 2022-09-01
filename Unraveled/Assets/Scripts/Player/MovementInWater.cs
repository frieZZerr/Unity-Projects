using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementInWater : MonoBehaviour
{
    public GameObject player;

    private PlayerMovement movement;
    float previousGravity;

    public bool inWater;

    private void Start()
    {
        movement = player.GetComponent<PlayerMovement>();
    }

    void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<AudioManager>().Play("Water Splash");
        previousGravity = movement.gravity;
        movement.gravity /= 3f;
        inWater = true;
    }

    void OnTriggerExit(Collider other)
    {
        movement.gravity = previousGravity;
        inWater = false;
    }
}
