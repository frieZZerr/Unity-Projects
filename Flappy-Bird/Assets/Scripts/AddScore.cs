using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddScore : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        Score.score++;
        FindObjectOfType<AudioManager>().Play("Score");
    }
}
