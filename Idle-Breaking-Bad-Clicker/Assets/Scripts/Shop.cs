using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject managers;
    public GameObject upgrades;

    public void Managers()
    {
        if (managers.activeSelf == false)
        {
            managers.SetActive(true);
            upgrades.SetActive(false);
        }
        else
        {
            managers.SetActive(false);
        }
    }

    public void Upgrades()
    {
        if (upgrades.activeSelf == false)
        {
            upgrades.SetActive(true);
            managers.SetActive(false);
        }
        else
        {
            upgrades.SetActive(false);
        }
    }

    public void Reputation()
    {
        PlayerData.money += 1000;
    }
}
