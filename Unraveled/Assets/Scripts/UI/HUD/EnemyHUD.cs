using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHUD : MonoBehaviour
{
    public Image hpBar;
    public EnemyStats stats;

    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        hpBar.fillAmount = (float)stats.currentHealth / (float)stats.health.GetValue();
    }
}
