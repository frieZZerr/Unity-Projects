using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Manager : MonoBehaviour
{
    public string managerName;
    public string businessName;
    public float managerCost;

    public TextMeshProUGUI managerNameText;
    public TextMeshProUGUI runsText;
    public TextMeshProUGUI managerCostText;
    public Button buy;

    void Start()
    {
        managerNameText.text = managerName;
        runsText.text = "Runs " + businessName;
        managerCostText.text = managerCost.ToString("0") + "$";
    }

    void Update()
    {
        if (PlayerData.money >= managerCost && PlayerData.ownedBusinesses.ContainsKey(businessName))
            buy.interactable = true;
        else
            buy.interactable = false;
    }

    public void HireManager()
    {
        PlayerData.money -= managerCost;
        PlayerData.ownedBusinesses[businessName].managment = true;
        PlayerData.ownedBusinesses[businessName].StartProduction();

        Destroy(gameObject);
    }
}
