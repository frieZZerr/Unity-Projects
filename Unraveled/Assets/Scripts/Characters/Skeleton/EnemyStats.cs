using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    public GameObject enemy;
    public PlayerManager player;

    public override void Die()
    {
        base.Die();

        enemy.GetComponent<Animator>().SetBool("Walk", false);
        enemy.GetComponent<Animator>().SetBool("Idle", false);
        enemy.GetComponent<Animator>().SetBool("isDead", true);

        enemy.GetComponent<EnemyAI>().enabled = false;
        enemy.GetComponent<CapsuleCollider>().enabled = false;

        player.GetComponent<PlayerStats>().GainXP(enemy.GetComponent<EnemyStats>().currentXp);
    }
}
