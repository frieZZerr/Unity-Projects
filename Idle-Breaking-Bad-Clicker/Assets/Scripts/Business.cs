using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Business : MonoBehaviour
{
    public string businessName;
    public float productionTime;
    public float baseCost;
    public float baseProduction;
    public float rateGrowth;
    public GameObject locked;

    [HideInInspector]
    public int quantity;
    [HideInInspector]
    public int milestone;
    [HideInInspector]
    public int lastMilestone;
    [HideInInspector]
    public int max;
    [HideInInspector]
    public int multipliers;
    [HideInInspector]
    public float production;
    [HideInInspector]
    public float upgradeCost;
    [HideInInspector]
    public float timer;
    [HideInInspector]
    public bool isActivated;
    [HideInInspector]
    public bool isWorking;
    [HideInInspector]
    public bool managment;

    int milestoneQuantity;

    /// <summary>
    /// This method activates the business
    /// also setting all the variables.
    /// </summary>
    public void ActivateBusiness()
    {
        isActivated = true;
        PlayerData.money -= baseCost;
        upgradeCost = baseCost;
        production = baseProduction;
        multipliers = 1;
        quantity = 1;
        milestone = 25;
        lastMilestone = 0;
        milestoneQuantity = 0;
        managment = false;
        timer = 0;
        locked.SetActive(false);
        PlayerData.ownedBusinesses.Add(businessName, this);
    }

    private void Update()
    {
        //  Nobody's gonna work if the business isn't activated
        if (isActivated)
        {
            //  Calculating the maximum number of investments that can be bought at the moment
            //      and how much money will it cost
            if (PlayerData.max)
            {
                float top = PlayerData.money * (rateGrowth - 1);
                float bot = Mathf.Pow(rateGrowth, quantity) * baseCost;
                float step = top / bot + 1;
                float log = Mathf.Log(step, rateGrowth);
                max = Mathf.FloorToInt(log);
                if (max == 0)
                    upgradeCost = baseCost * Mathf.Pow(rateGrowth, quantity-1);
                else
                    upgradeCost = baseCost * Mathf.Pow(rateGrowth, quantity-1) * (Mathf.Pow(rateGrowth, max) - 1) / (rateGrowth - 1);
            }
            else if(PlayerData.next)
            {
                upgradeCost = baseCost * Mathf.Pow(rateGrowth, quantity - 1) * (Mathf.Pow(rateGrowth, milestone-quantity) - 1) / (rateGrowth - 1);
            }
            else
            {
                upgradeCost = baseCost * Mathf.Pow(rateGrowth, quantity-1) * ( Mathf.Pow(rateGrowth, PlayerData.quantity) - 1) / (rateGrowth - 1);
            }

            //  Calculating current production
            production = (baseProduction * quantity) * multipliers;

            CheckMilestones();

            //  The business is working
            if (isWorking)
            {
                timer += Time.deltaTime;

                //  When the production is done
                if (timer > productionTime)
                {
                    timer = 0f;
                    OnProduced();
                    isWorking = false;

                    //  Hiring a manager makes your business much easier
                    //      since they'll automatically start the next batch
                    if (managment)
                        StartProduction();
                }
            }
        }
    }

    /// <summary>
    /// This method starts the production.
    /// </summary>
    public void StartProduction()
    {
        if (!isWorking && isActivated)
        {
            isWorking = true;
            //FindObjectOfType<AudioManager>().Play(businessName);
        }
    }

    /// <summary>
    /// This method upgrades the business by
    /// making the production more profitable
    /// (also you need to pay for it).
    /// </summary>
    public void Upgrade()
    {
        PlayerData.money -= upgradeCost;
        if (PlayerData.max)
            quantity += max;
        else if (PlayerData.next)
            quantity += (milestone - quantity);
        else
            quantity += PlayerData.quantity;
    }

    /// <summary>
    /// This method tries to activate the business.
    /// </summary>
    public void TryActivateBusiness()
    {
        //  If all the arrangments are ready,
        //      it's time to open the business
        if (PlayerData.money >= baseCost && !isActivated)
            ActivateBusiness();
    }

    /// <summary>
    /// This method adds money to your wallet.
    /// </summary>
    void OnProduced()
    {
        PlayerData.money += production;
    }

    /// <summary>
    /// This method checks if the business reached a milestone.
    /// If it did, it adds a production bonus and sets another milestone.
    /// </summary>
    void CheckMilestones()
    {
        if (quantity >= milestone)
        {
            productionTime /= 2f;

            milestoneQuantity++;
            if (milestoneQuantity >= milestone/5)
            {
                milestone *= 2;
            }

            lastMilestone = milestone;
            milestone += milestone;
        }
    }
}
