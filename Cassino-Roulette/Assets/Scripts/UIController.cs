using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;
using System.Linq;

public class UIController : MonoBehaviour
{
    public PlayerBalance playerBalance;
    public WheelController wheelController;

    public TextMeshProUGUI balanceText;
    public TextMeshProUGUI currentBetText;
    public TMP_InputField inputField;

    public List<GameObject> lastWins;

    Color Red   = new Color( 197f/255f, 48f/255f,  13f/255f, 250f / 255f );
    Color Black = new Color( 26f/255f,  26f/255f,  26f/255f, 250f / 255f );
    Color Green = new Color(    0f,     147f/255f, 69f/255f, 250f / 255f );

    // Update is called once per frame
    void Update()
    {
        balanceText.text = "Balance: " + playerBalance.balance.ToString() + "$";
        currentBetText.text = "Current Bet: " + playerBalance.currentBetValue.ToString() + "$";
    }

    //  Updating the last wins UI
    public void UpdateLastWins()
    {
        Queue<int> temp = wheelController.winningQueue;

        int i = temp.Count-1;
        foreach( var elem in temp)
        {
            lastWins[i].GetComponent<Image>().color = SetColor(elem);
            lastWins[i].GetComponentInChildren<TextMeshProUGUI>().text = elem.ToString();
            i--;
        }
    }

    //  Controlling the bet layout
    //  TO-DO:
    //      - Make adding and removing bets in another function
    public void BetChoice( string choice )
    {
        //  Can't place bets when the wheel is spinning
        if (wheelController.isSpinning) return;

        //  Get the selected button and set appropriate list
        GameObject chosenButton = EventSystem.current.currentSelectedGameObject;

        string Tkey = chosenButton.name;
        List<int> Tvalue = GetSelectedList(Tkey);

        //  Checking if the chosen bet has been already selected
        bool isPlaced = false;
        foreach( var bet in playerBalance.betterPlacedBets )
        {
            if( bet.Key == Tkey )
            {
                isPlaced = true;
                break;
            }
        }

        //  If player has selected the same bet twice
        if (isPlaced)
        {
            //  Remove selected button from selected list and disable it's image
            playerBalance.betterPlacedBets.Remove(Tkey);
            chosenButton.gameObject.GetComponent<Image>().color = new Color(255f,255f,255f,0f);

            if( playerBalance.placedBetsValues.ContainsKey(Tkey) )
            {
                //  Return this bet's value to player
                int val = playerBalance.placedBetsValues[Tkey];
                if( val > 0 && playerBalance.currentBetValue > 0 )
                {
                    playerBalance.balance += val;
                    playerBalance.currentBetValue -= val;
                }
                playerBalance.placedBetsValues.Remove(Tkey);
            }
        }
        else
        {
            //  Add selected button to selected list and enable it's image
            playerBalance.betterPlacedBets.Add(Tkey, Tvalue);
            chosenButton.gameObject.GetComponent<Image>().color = new Color(255f, 255f, 255f, 120f / 255f);

            playerBalance.placedBetsValues.Add(Tkey, playerBalance.Bet());
        }

        FindObjectOfType<AudioManager>().Play("Click");
    }

    //  Setting appropriate color
    Color SetColor( int number )
    {
        if (number == 0)
            return Green;
        else
        {
            if ( GetSelectedList("Black").Contains(number) )
                return Black;
            else
                return Red;
        }
    }

    //  Returns list with selected numbers
    List<int> GetSelectedList( string name )
    {
        switch(name)
        {
            case "Red":
                return new List<int> { 1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36 };
            case "Black":
                return new List<int> { 2, 4, 6, 8, 10, 11, 13, 15, 17, 20, 22, 24, 26, 28, 29, 31, 33, 35 };
            case "Odd":
                return new List<int> { 1, 3, 5, 7, 9, 11, 13, 15, 17, 19, 21, 23, 25, 27, 29, 31, 33, 35 };
            case "Even":
                return new List<int> { 0, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30, 32, 34, 36 };
            case "OneTo18":
                return new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 };
            case "NineteenTo36":
                return new List<int> { 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36 };
            case "First12":
                return new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            case "Second12":
                return new List<int> { 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 };
            case "Third12":
                return new List<int> { 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36 };
            case "TwoToOne3":
                return new List<int> { 3, 6, 9, 12, 15, 18, 21, 24, 27, 30, 33, 36 };
            case "TwoToOne2":
                return new List<int> { 2, 5, 8, 11, 14, 17, 20, 23, 26, 29, 32, 35 };
            case "TwoToOne1":
                return new List<int> { 1, 4, 7, 10, 13, 16, 19, 22, 25, 28, 31, 34 };
            default:
                return new List<int> { int.Parse(name) };
        }
    }

    //  Managing the betting field UI
    public void BetValue( string action )
    {
        FindObjectOfType<AudioManager>().Play("Click");

        //  Get current input field's bet value and player's balance
        int value = int.Parse(inputField.text.Trim('$'));
        int balance = playerBalance.balance;

        switch (action)
        {
            case "Max":
                action = balance.ToString();
                break;

            case "x2":
                int temp = value * 2;
                if (temp > balance)
                    action = balance.ToString();
                else
                    action = temp.ToString();
                break;

            case "1/2":
                temp = value / 2;
                action = temp.ToString();
                break;

            case "+1k":
                if (balance < 1000)
                    action = balance.ToString();
                else
                {
                    temp = value + 1000;
                    if (temp > balance)
                        action = balance.ToString();
                    else
                        action = temp.ToString();
                }
                break;

            case "+100":
                if (balance < 100)
                    action = balance.ToString();
                else
                {
                    temp = value + 100;
                    if(temp > balance)
                        action = balance.ToString();
                    else
                        action = temp.ToString();
                }
                break;

            case "+10":
                if (balance < 10)
                    action = balance.ToString();
                else
                {
                    temp = value + 10;
                    if(temp > balance)
                        action = balance.ToString();
                    else
                        action = temp.ToString();
                }
                break;

            case "LastBet":
                if (balance < playerBalance.lastBetValue)
                    action = balance.ToString();
                else
                    action = playerBalance.lastBetValue.ToString();
                break;

            case "Clear":
                action = "0";
                break;

            default:
                break;
        }

        inputField.text = action;
        UpdateInputField();
    }

    //  Updating input field's text
    public void UpdateInputField()
    {
        //  Checking if entered text is correct
        char[] arr = inputField.text.ToCharArray();
        foreach( char c in arr )
        {
            //  If there's any character != numbers
            if( c < 48 || c > 57 )
            {
                inputField.text = "0";
                break;
            }
        }

        //  Adding '$' to the value
        if (inputField.text.Trim('$') == inputField.text)
            inputField.text += "$";
    }
}
