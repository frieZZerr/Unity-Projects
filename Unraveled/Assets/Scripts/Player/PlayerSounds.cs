using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public void Attack_1_Sound()
    {
        FindObjectOfType<AudioManager>().Play("Player Attack1");
    }

    public void Attack_2_Sound()
    {
        FindObjectOfType<AudioManager>().Play("Player Attack2");
    }

    public void Attack_3_Sound()
    {
        FindObjectOfType<AudioManager>().Play("Player Attack3");
    }

    public void Attack_4_Sound()
    {
        FindObjectOfType<AudioManager>().Play("Player Attack4");
    }
}
