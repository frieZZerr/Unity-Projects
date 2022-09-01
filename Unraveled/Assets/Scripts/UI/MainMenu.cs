using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public void Continue()
    {
        SceneManager.LoadScene(1);

        //  Load last save
    }

    public void NewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        //  New Game UI
        //  *Character creator
    }

    public void LoadGame()
    {
        //  Load Game UI
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exit Game");
    }
}
