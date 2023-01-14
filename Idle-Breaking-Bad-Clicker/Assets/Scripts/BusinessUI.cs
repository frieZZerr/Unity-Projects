using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BusinessUI : MonoBehaviour
{
    public Slider slider;
    public Slider milestones;
    public TextMeshProUGUI upgradeCost;
    public TextMeshProUGUI time;
    public TextMeshProUGUI production;
    public TextMeshProUGUI quantity;
    public TextMeshProUGUI businessName;
    public TextMeshProUGUI unlock;
    public TextMeshProUGUI max;
    public Button upgrade;
    public Button activate;

    Business business;

    void Start()
    {
        business = GetComponent<Business>();
        businessName.text = business.name;
        unlock.text = "Unlock for " + business.baseCost.ToString("0") + "$";
    }

    void Update()
    {
        if (business.isActivated)
        {
            slider.value = business.timer / business.productionTime;
            milestones.minValue = business.lastMilestone;
            milestones.maxValue = business.milestone;
            milestones.value = business.quantity;
            upgradeCost.text = business.upgradeCost.ToString("0.00") + "$";
            production.text = business.production.ToString("0.00") + "$";
            time.text = (business.productionTime - business.timer).ToString("0") + "s";
            quantity.text = business.quantity.ToString();

            if (PlayerData.max)
                max.text = "x" + business.max.ToString();
            else if (PlayerData.next)
            {
                max.text = "x" + (business.milestone - business.quantity).ToString();
            }
            else
                max.text = "x" + PlayerData.quantity;

            if (PlayerData.money < business.upgradeCost)
            {
                upgrade.interactable = false;
            }
            else
            {
                upgrade.interactable = true;
            }
        }
        else
        {
            if (PlayerData.money >= business.baseCost)
            {
                activate.interactable = true;
            }
            else
            {
                activate.interactable = false;
            }
        }
    }
}
