using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI macroText;

    void Update()
    {
        moneyText.text = PlayerData.money.ToString("0.00") + "$";
    }

    public void UpdateMacro()
    {
        if (PlayerData.max)
        {
            PlayerData.quantity = 1;
            macroText.text = "x" + PlayerData.quantity.ToString();
            PlayerData.max = false;
            return;
        }

        if (PlayerData.quantity >= 100 && !PlayerData.next)
        {
            macroText.text = "NEXT";
            PlayerData.next = true;
            return;
        }

        if (PlayerData.next)
        {
            macroText.text = "MAX";
            PlayerData.next = false;
            PlayerData.max = true;
            return;
        }

        PlayerData.quantity *= 10;
        macroText.text = "x" + PlayerData.quantity.ToString();
    }
}
