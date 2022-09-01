using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    public float attackSpeed = 1f;
    public float attackDelay = 0.5f;
    private float attackCooldown = 0f;

    CharacterStats myStats;

    private void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }

    private void Update()
    {
        attackCooldown -= Time.deltaTime;
    }

    public void Attack(CharacterStats targetStats)
    {
        if(attackCooldown <= 0f)
        {
            StartCoroutine(DoDamage(targetStats, attackDelay));
            attackCooldown = 1f * attackSpeed;
        }
    }

    IEnumerator DoDamage(CharacterStats stats, float delay)
    {
        yield return new WaitForSeconds(delay);

        stats.TakeDamage(myStats.damage.GetValue());
    }    
}
