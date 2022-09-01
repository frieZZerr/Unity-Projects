using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int currentHealth { get; private set; }
    public int currentXp { get; private set; }

    public Stat health;
    public Stat damage;
    public Stat armor;
    public Stat xp;

    void Awake()
    {
        currentHealth = health.GetValue();
        currentXp = xp.GetValue();
    }

    public void TakeDamage(int damage)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(0, damage, int.MaxValue);

        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public void GainXP(int XP)
    {
        currentXp += XP;

        Debug.Log(transform.name + " gains " + XP + " expirience.");
    }

    public virtual void Die()
    {
        //  Die
        Debug.Log(transform.name + " died.");
    }
}
