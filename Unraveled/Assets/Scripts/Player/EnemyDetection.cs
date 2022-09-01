using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public CharacterStats enemyStats;

    public bool inRange;

    private void Start()
    {
        if (GetComponent<CharacterStats>() == null)
            return;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            inRange = true;
            enemyStats = other.GetComponent<CharacterStats>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            inRange = false;
        }
    }
}
