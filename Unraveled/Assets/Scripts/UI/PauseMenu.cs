using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject PauseMenuUI;
    public GameObject SettingsMeneUI;
    public GameObject Crosshair;

    public PlayerManager player;
    public GameObject camera;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        PauseMenuUI.SetActive(false);
        SettingsMeneUI.SetActive(false);
        Crosshair.SetActive(true);
        player.GetComponent<PlayerCombat>().enabled = true;
        camera.GetComponent<Cinemachine.CinemachineBrain>().m_UpdateMethod = Cinemachine.CinemachineBrain.UpdateMethod.SmartUpdate;
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void SaveGame()
    {

    }

    public void LoadGame()
    {

    }

    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        PauseMenuUI.SetActive(true);
        SettingsMeneUI.SetActive(false);
        Crosshair.SetActive(false);
        player.GetComponent<PlayerCombat>().enabled = false;
        camera.GetComponent<Cinemachine.CinemachineBrain>().m_UpdateMethod = Cinemachine.CinemachineBrain.UpdateMethod.FixedUpdate;
        Time.timeScale = 0f;
        isPaused = true;
    }
}
