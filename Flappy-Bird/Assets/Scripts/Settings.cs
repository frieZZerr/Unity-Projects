using UnityEngine;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    public AudioMixer mixer;
    public Animator animator;
    public GameObject mutedImage;

    public GameObject lightBackground;
    public GameObject darkBackground;

    public GameObject lightPipe;
    public GameObject darkPipe;

    public static string selectedSkin = "Yellow";
    public static string selectedMode = "Light";
    public static bool muted = false;

    public void SetSettings()
    {
        SelectSkin(selectedSkin);
        SelectMode(selectedMode);
    }

    public void SelectSkin (string skin)
    {
        selectedSkin = skin;
        OnSelectSkin();
    }

    public void SelectMode(string mode)
    {
        selectedMode = mode;
        OnSelectMode();
    }
    public void SelectMute()
    {
        if (muted)
        {
            muted = false;
            mutedImage.SetActive(false);
            mixer.SetFloat("Volume", 0f);
        }
        else
        {
            muted = true;
            mutedImage.SetActive(true);
            mixer.SetFloat("Volume", -80f);
        }
    }

    private void OnSelectSkin()
    {
        animator.SetTrigger(selectedSkin);
    }

    private void OnSelectMode()
    {
        if (selectedMode == "Light")
        {
            lightBackground.SetActive(true);
            darkBackground.SetActive(false);
            WalSpawner.pipe = lightPipe;
        }
        else
        {
            lightBackground.SetActive(false);
            darkBackground.SetActive(true);
            WalSpawner.pipe = darkPipe;
        }
    }
}
