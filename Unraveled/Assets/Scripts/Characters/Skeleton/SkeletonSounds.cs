using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSounds : MonoBehaviour
{
    public void Attack_1_Sound()
    {
        FindObjectOfType<AudioManager>().Play("Skeleton Attack1");
    }

    public void Hurt_1_Sound()
    {
        FindObjectOfType<AudioManager>().Play("Skeleton Hurt1");
    }

    public void Die_1_Sound()
    {
        FindObjectOfType<AudioManager>().Play("Skeleton Die1");
    }
}
