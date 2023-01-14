using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Upgrade : MonoBehaviour
{
    public string upgradeName;
    public string businessName;
    public int multiplier;
    public float upgradeCost;

    public TextMeshProUGUI upgradeNameText;
    public TextMeshProUGUI multiplierText;
    public TextMeshProUGUI upgradeCostText;
    public Button buy;

    void Start()
    {
        upgradeNameText.text = upgradeName;
        multiplierText.text = businessName + " profit x" + multiplier.ToString("0");
        upgradeCostText.text = upgradeCost.ToString("0") + "$";
    }

    void Update()
    {
        if (PlayerData.money >= upgradeCost && PlayerData.ownedBusinesses.ContainsKey(businessName))
            buy.interactable = true;
        else
            buy.interactable = false;
    }

    public void BuyUpgrade()
    {
        PlayerData.money -= upgradeCost;
        PlayerData.ownedBusinesses[businessName].multipliers *= 3;

        Destroy(gameObject);
    }
}
