using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static int score;
    public static int bestScore;
    public static bool newBest = false;

    void Start()
    {
        score = 0;

        //  BEST SCORE
        if (PlayerPrefs.GetInt("Best", -1) == -1)
            bestScore = 0;
        else
            bestScore = PlayerPrefs.GetInt("Best");
    }

    void Update()
    {
        GetComponent<TMPro.TextMeshProUGUI>().text = score.ToString();
    }

    public static void UpdateBestScore()
    {
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("Best", bestScore);
            newBest = true;
        }
    }
}
