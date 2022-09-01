using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class PlayerBalance : MonoBehaviour
{
    public WheelController wheelController;

    //  Holds all bets player has selected
    public Dictionary<string, List<int>> betterPlacedBets = new Dictionary<string, List<int>>();
    //  Holds values player has bet on selected bets
    public Dictionary<string, int> placedBetsValues = new Dictionary<string, int>();
    public int balance;
    public int currentBetValue;
    public int lastBetValue;

    public bool hasWon;

    public TMP_InputField inputField;

    // Start is called before the first frame update
    void Start()
    {
        balance = 1000;
        currentBetValue = 0;
        lastBetValue = currentBetValue;
        hasWon = false;
    }

    public int Bet()
    {
        //  Can't place bets when the wheel is spinning
        if (wheelController.isSpinning) return 0;

        int betValue = int.Parse(inputField.text.Trim('$'));

        //  Can't bet negative values or more than the player currently has
        if (betValue > balance || betValue <= 0) return 0;

        currentBetValue += betValue;
        lastBetValue = currentBetValue;
        balance -= betValue;

        return betValue;
    }

    public void CheckWin(int win)
    {
        //  Checking if the player has won
        foreach (var bet in betterPlacedBets)
        {
            if (bet.Value.Contains(win))
            {
                int val = placedBetsValues[bet.Key];
                balance += val * GetMultiplier(bet.Key);
                hasWon = true;
            }
        }
    }

    //  Preparing things for the next bet
    public void Reset()
    {
        currentBetValue = 0;
        inputField.text = "0$";
        hasWon = false;
    }

    //  Set appropriate multiplier according to placed bet
    private int GetMultiplier(string key)
    {
        if (key == "OneTo18" || key == "Odd" || key == "Red" ||
            key == "Black" || key == "Even" || key == "NineteenTo36")
            return 2;
        else if (key == "First12" || key == "Second12" || key == "Third12" ||
                 key == "TwoToOne1" || key == "TwoToOne2" || key == "TwoToOne3")
            return 3;
        else
            return 36;
    }
}
