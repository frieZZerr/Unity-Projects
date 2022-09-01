using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public UIController uiController;
    public WalSpawner spawner;
    public BridMove bridMove;
    public Rigidbody2D rb;
    public Settings settings;

    [HideInInspector]
    public static bool gameStarted;
    bool paused;

    private void Start()
    {
        Time.timeScale = 1;

        paused = false;
        spawner.enabled = false;
        rb.gravityScale = 0f;
        Score.newBest = false;
        gameStarted = false;
    }

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (uiController.startCanvas.activeSelf == true)
            {
                gameStarted = true;
                spawner.enabled = true;
                rb.gravityScale = 0.6f;
                uiController.StartGame();
                bridMove.Tap();
            }

            if (paused)
            {
                uiController.Unpause();
                bridMove.Tap();
                paused = false;
                Time.timeScale = 1;
            }
        }
    }

    public void GameOver()
    {
        uiController.GameOver();
        Time.timeScale = 0;
    }

    public void Pause()
    {
        Time.timeScale = 0;
        paused = true;
    }


    public void Replay()
    {
        SceneManager.LoadScene(0);
    }
}
