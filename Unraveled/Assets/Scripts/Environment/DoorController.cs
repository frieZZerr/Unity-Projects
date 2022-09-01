using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animator doorAnim;

    private bool doorOpened = false;

    private void Awake()
    {
        doorAnim = gameObject.GetComponent<Animator>();
    }

    public void PlayAnimation()
    {
        if (!doorOpened)
        {
            doorAnim.SetBool("isOpening", true);
            //doorAnim.Play("DoorOpen", 0, 0.0f);
            FindObjectOfType<AudioManager>().Play("Door Open");
            doorOpened = true;
        }
        else
        {
            doorAnim.SetBool("isOpening", false);
            FindObjectOfType<AudioManager>().Play("Door Close");
            //doorAnim.Play("DoorClose", 0, 0.0f);
            doorOpened = false;
        }
    }
}
