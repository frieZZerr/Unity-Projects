using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class UIController : MonoBehaviour
{
    public Settings settings;
    public GameObject gameOverCanvas;
    public GameObject scoreCanvas;
    public GameObject startCanvas;
    public GameObject menuCanvas;
    public Animator animator;

    public GameObject paused;
    public Image medal;
    public Image newBest;
    public List<Sprite> medalList;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;
    public Toggle toggle;

    private void Awake()
    {
        settings.SetSettings();

        if (Settings.muted)
        {
            Settings.muted = !Settings.muted;
            settings.SelectMute();
        }
    }

    private void Start()
    {
        if (GameManager.gameStarted)
        {
            Play();
        }
    }

    public void Play()
    {
        menuCanvas.SetActive(false);
        startCanvas.SetActive(true);
        scoreCanvas.SetActive(true);
        animator.Play("Start");
    }

    public void StartGame()
    {
        startCanvas.SetActive(false);
        scoreCanvas.SetActive(true);
    }

    public void GameOver()
    {
        Score.UpdateBestScore();
        scoreText.text = Score.score.ToString();
        bestScoreText.text = Score.bestScore.ToString();

        SetMedal(Score.score);
        if (Score.newBest)
            newBest.enabled = true;

        FindObjectOfType<AudioManager>().Play("Death");

        gameOverCanvas.SetActive(true);
        scoreCanvas.SetActive(false);

        animator.Play("GameOver");
    }

    public void Unpause()
    {
        paused.SetActive(false);
    }

    public void Menu()
    {
        GameManager.gameStarted = false;
        SceneManager.LoadScene(0);
        animator.Play("Menu");
    }

    void SetMedal(int score)
    {
        if (score >= 5 && score < 10)   { medal.sprite = medalList[0]; }
        if (score >= 10 && score < 25)  { medal.sprite = medalList[1]; }
        if (score >= 25 && score < 50)  { medal.sprite = medalList[2]; }
        if (score >= 50)                { medal.sprite = medalList[3]; }
    }
}
