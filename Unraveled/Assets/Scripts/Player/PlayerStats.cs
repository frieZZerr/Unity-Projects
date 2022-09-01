using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    public PlayerManager player;

    public override void Die()
    {
        base.Die();

        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<TPPMovement>().enabled = false;
        player.GetComponent<PlayerCombat>().enabled = false;
        player.GetComponent<CharacterCombat>().enabled = false;

        player.GetComponent<Animator>().Play("Die");
        FindObjectOfType<AudioManager>().Play("Player Die1");
    }
}
