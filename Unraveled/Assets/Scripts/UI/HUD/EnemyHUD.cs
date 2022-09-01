using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHUD : MonoBehaviour
{
    public Image hpBar;

    public EnemyStats stats;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        hpBar.fillAmount = stats.currentHealth / stats.health.GetValue();
    }
}
