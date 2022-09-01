using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WheelController : MonoBehaviour
{
    public PlayerBalance playerBalance;
    public UIController uiController;
    public AudioManager audioManager;

    static int[] values = { 0, 32, 15, 19, 4, 21, 2, 25, 17, 34,
                            6, 27, 13, 36, 11, 30, 8, 23, 10, 5,
                            24, 16, 33, 1, 20, 14, 31, 9, 22, 18,
                            29, 7, 28, 12, 35, 3, 26 };

    //  Number of degrees of 1 slot in roulette
    static float sliceDeg = 360f / 37f;
    //  The wheel needs to be shifted half the
    //      degrees of 1 slot because of the sprite:
    //      - Normal rotation (that is (0,0,0)) makes
    //          the 0-slot centered so we need to shift it
    static float shift = sliceDeg / 2f;
    int winningSlot;
    [HideInInspector]
    public int winningNumber;
    public Queue<int> winningQueue = new Queue<int>();

    float speed;
    float currentSpeed;

    [HideInInspector]
    public bool isSpinning;
    bool justStopped;

    // Start is called before the first frame update
    void Start()
    {
        ResetValues();
        winningNumber = 0;
        justStopped = false;
    }

    // Update is called once per frame
    void Update()
    {
        //  If the wheel is spinning
        if (isSpinning && currentSpeed > 0f)
        {
            //  Spin the wheel
            transform.Rotate(0, 0, currentSpeed * Time.deltaTime);
            currentSpeed -= 1f;

            //  Play spin sound
            float multiplierSpeed = (0.1f+Array.Find(FindObjectOfType<AudioManager>().sounds, sound => sound.name == "OneSpin").clip.length) * (1.1f-currentSpeed/speed);
            if (audioManager.canPlay)
                StartCoroutine(audioManager.Wait(multiplierSpeed));

            //  If current speed is <= 0 it means
            //      that the wheel has just stopped
            if (currentSpeed <= 0f)
                justStopped = true;
        }
        else    //  If it is not spinning
        {
            //  If the wheel has just stopped spinning
            if( justStopped )
            {

                //  Get the winning slot
                winningSlot = (int)Mathf.Floor((transform.eulerAngles.z + shift) / sliceDeg);
                winningNumber = values[winningSlot];

                //  Updating last wins and UI
                winningQueue.Enqueue(winningNumber);
                if (winningQueue.Count > 8)
                    winningQueue.Dequeue();
                uiController.UpdateLastWins();

                //  Check if player has bet the winning slot
                playerBalance.CheckWin(winningNumber);

                //  Play sound
                if (playerBalance.currentBetValue == 0)
                    FindObjectOfType<AudioManager>().Play("PassiveBet");
                else
                {
                    if (playerBalance.hasWon)
                        FindObjectOfType<AudioManager>().Play("Won");
                    else
                        FindObjectOfType<AudioManager>().Play("Lost");
                }

                playerBalance.Reset();

                justStopped = false;
            }

            //  Prepare the wheel for next spin
            ResetValues();
        }
    }

    private void ResetValues()
    {
        speed = UnityEngine.Random.Range(1000, 1750);
        currentSpeed = speed;
        isSpinning = false;
    }

    public void Spin()
    {
        if (isSpinning) return;

        //  Play startingSpin sound
        FindObjectOfType<AudioManager>().Play("StartSpinning");

        isSpinning = true;
    }
}
