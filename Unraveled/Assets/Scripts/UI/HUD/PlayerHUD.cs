using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public PlayerManager player;
    public Image healthBar;
    public Image xpBar;

    PlayerStats stats;

    void Start()
    {
        stats = player.GetComponent<PlayerStats>();
    }

    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        float fill = (float)stats.currentHealth / (float)stats.health.GetValue();
        healthBar.fillAmount = fill;
        xpBar.fillAmount = stats.currentXp / 100f;
    }
}
