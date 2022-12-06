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
    public List<Image> newMedals;
    public List<TextMeshProUGUI> medalCountList;
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

        if (PlayerPrefs.GetInt("Bronze", -1) == -1)
            PlayerPrefs.SetInt("Bronze", 0);
        if (PlayerPrefs.GetInt("Silver", -1) == -1)
            PlayerPrefs.SetInt("Silver", 0);
        if (PlayerPrefs.GetInt("Gold", -1) == -1)
            PlayerPrefs.SetInt("Gold", 0);
        if (PlayerPrefs.GetInt("Platinum", -1) == -1)
            PlayerPrefs.SetInt("Platinum", 0);

        medalCountList[0].text = "x" + PlayerPrefs.GetInt("Bronze").ToString();
        medalCountList[1].text = "x" + PlayerPrefs.GetInt("Silver").ToString();
        medalCountList[2].text = "x" + PlayerPrefs.GetInt("Gold").ToString();
        medalCountList[3].text = "x" + PlayerPrefs.GetInt("Platinum").ToString();

        if(PlayerPrefs.GetString("NewBronze", "null") == "null" )
            PlayerPrefs.SetString("NewBronze", "false");

        if (PlayerPrefs.GetString("NewSilver", "null") == "null")
            PlayerPrefs.SetString("NewSiler", "false");

        if (PlayerPrefs.GetString("NewGold", "null") == "null")
            PlayerPrefs.SetString("NewGold", "false");

        if (PlayerPrefs.GetString("NewPlatinum", "null") == "null")
            PlayerPrefs.SetString("NewPlatinum", "false");
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

    public void Stats()
    {
        if (PlayerPrefs.GetString("NewBronze") == "true")
            newMedals[0].gameObject.SetActive(true);
        if (PlayerPrefs.GetString("NewSilver") == "true")
            newMedals[1].gameObject.SetActive(true);
        if (PlayerPrefs.GetString("NewGold") == "true")
            newMedals[2].gameObject.SetActive(true);
        if (PlayerPrefs.GetString("NewPlatinum") == "true")
            newMedals[3].gameObject.SetActive(true);
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
        if (score >= 5 && score < 10) {
            medal.sprite = medalList[0];
            PlayerPrefs.SetString("NewBronze", "true");
            PlayerPrefs.SetInt("Bronze", PlayerPrefs.GetInt("Bronze") + 1 );
            medalCountList[0].text = "x"+PlayerPrefs.GetInt("Bronze").ToString();
        }
        if (score >= 10 && score < 25) {
            medal.sprite = medalList[1];
            PlayerPrefs.SetString("NewSilver", "true");
            PlayerPrefs.SetInt("Silver", PlayerPrefs.GetInt("Silver") + 1);
            medalCountList[1].text = "x" + PlayerPrefs.GetInt("Silver").ToString();
        }
        if (score >= 25 && score < 50) {
            medal.sprite = medalList[2];
            PlayerPrefs.SetString("NewGold", "true");
            PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold") + 1);
            medalCountList[2].text = "x" + PlayerPrefs.GetInt("Gold").ToString();
        }
        if (score >= 50) {
            medal.sprite = medalList[3];
            PlayerPrefs.SetString("NewPlatinum", "true");
            PlayerPrefs.SetInt("Platinum", PlayerPrefs.GetInt("Platinum") + 1);
            medalCountList[3].text = "x" + PlayerPrefs.GetInt("Platinum").ToString();
        }
    }

    public void ResetNewMedals()
    {
        PlayerPrefs.SetString( "NewBronze",   "false");
        PlayerPrefs.SetString( "NewSilver",   "false");
        PlayerPrefs.SetString( "NewGold",     "false");
        PlayerPrefs.SetString( "NewPlatinum", "false");
    }
}
