using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static int score;
    public static int bestScore = 0;
    public static bool newBest = false;

    void Start()
    {
        score = 0;
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
            newBest = true;
        }
    }
}
