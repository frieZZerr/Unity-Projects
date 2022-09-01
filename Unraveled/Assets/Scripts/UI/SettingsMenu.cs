using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.Universal;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public TMP_Text masterVolumeText;
    public TMP_Text musicVolumeText;
    public TMP_Text SFXVolumeText;
    public TMP_Text voiceVolumeText;

    Resolution[] resolutions;
    public TMP_Dropdown resolutionsDropdown;

    public TMP_Dropdown qualityDropdown;
    public TMP_Dropdown textureQualityDropdown;
    public TMP_Dropdown antiAliasingDropdown;
    public TMP_Dropdown renderScaleDropdown;

    public Toggle fullscreenToggle;
    public Toggle HDRToggle;
    public Toggle VSyncToggle;
    public Toggle depthTextureToggle;

    public UniversalRenderPipelineAsset[] qualityLevel;
    public int currentLevel = 3;

    private void Start()
    {
        //  START OPTIONS
        SetQuality(3);
        qualityDropdown.value = 3;
        fullscreenToggle.isOn = Screen.fullScreen;

        //  RESOLUTION
        resolutions = Screen.resolutions;

        resolutionsDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }

        resolutionsDropdown.AddOptions(options);
        resolutionsDropdown.value = currentResolutionIndex;
        resolutionsDropdown.RefreshShownValue();
    }

    //////////  AUDIO  //////////

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log(volume) * 20f);
        masterVolumeText.SetText(Mathf.Round(volume * 100f).ToString() + " %");
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log(volume) * 20f);
        musicVolumeText.SetText(Mathf.Round(volume * 100f).ToString() + " %");
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log(volume) * 20f);
        SFXVolumeText.SetText(Mathf.Round(volume * 100f).ToString() + " %");
    }

    public void SetVoiceVolume(float volume)
    {
        audioMixer.SetFloat("VoiceVolume", Mathf.Log(volume) * 20f);
        voiceVolumeText.SetText(Mathf.Round(volume * 100f).ToString() + " %");
    }

            //////////  GRAPHICS  //////////

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        currentLevel = QualitySettings.GetQualityLevel();

        switch (qualityIndex)
        {
            //  LOW
            case 0:
                SetTextureQuality(0);
                textureQualityDropdown.value = 0;

                SetHDR(false);
                HDRToggle.isOn = false;

                SetShadowDistance(0);
                antiAliasingDropdown.value = 0;

                SetVSync(false);
                VSyncToggle.isOn = false;

                SetRenderScale(0);
                renderScaleDropdown.value = 0;

                SetDepthTexture(false);
                depthTextureToggle.isOn = false;

                break;

            //  MEDIUM
            case 1:
                SetTextureQuality(1);
                textureQualityDropdown.value = 1;

                SetHDR(false);
                HDRToggle.isOn = false;

                SetShadowDistance(1);
                antiAliasingDropdown.value = 1;

                SetVSync(true);
                VSyncToggle.isOn = true;

                SetRenderScale(1);
                renderScaleDropdown.value = 1;

                SetDepthTexture(false);
                depthTextureToggle.isOn = false;

                break;

            //  HIGH
            case 2:
                SetTextureQuality(2);
                textureQualityDropdown.value = 2;

                SetHDR(true);
                HDRToggle.isOn = true;

                SetShadowDistance(2);
                antiAliasingDropdown.value = 2;

                SetVSync(true);
                VSyncToggle.isOn = true;

                SetRenderScale(2);
                renderScaleDropdown.value = 2;

                SetDepthTexture(false);
                depthTextureToggle.isOn = false;

                break;

            //  ULTRA
            case 3:
                SetTextureQuality(2);
                textureQualityDropdown.value = 2;

                SetHDR(true);
                HDRToggle.isOn = true;

                SetShadowDistance(3);
                antiAliasingDropdown.value = 3;

                SetVSync(true);
                VSyncToggle.isOn = true;

                SetRenderScale(2);
                renderScaleDropdown.value = 2;

                SetDepthTexture(true);
                depthTextureToggle.isOn = true;

                break;
        }
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetTextureQuality(int textureQualityIndex)
    {
        QualitySettings.masterTextureLimit = Mathf.Abs(textureQualityIndex - 2);
    }

    public void SetHDR(bool isHDR)
    {
        qualityLevel[currentLevel].supportsHDR = isHDR;
    }

    public void SetShadowDistance(int shadowDistanceIndex)
    {
        switch (shadowDistanceIndex)
        {
            case 0:
                qualityLevel[currentLevel].shadowDistance = 50f;
                break;

            case 1:
                qualityLevel[currentLevel].shadowDistance = 100f;
                break;

            case 2:
                qualityLevel[currentLevel].shadowDistance = 175f;
                break;

            case 3:
                qualityLevel[currentLevel].shadowDistance = 250f;
                break;
        }
    }

    public void SetVSync(bool isVSync)
    {
        if (isVSync)
            QualitySettings.vSyncCount = 1;
        else
            QualitySettings.vSyncCount = 0;
    }

    public void SetRenderScale(int renderScaleIndex)
    {
        switch(renderScaleIndex)
        {
            case 0:
                qualityLevel[currentLevel].renderScale = 0.5f;
                break;

            case 1:
                qualityLevel[currentLevel].renderScale = 0.85f;
                break;

            case 2:
                qualityLevel[currentLevel].renderScale = 1f;
                break;
        }
    }

    public void SetDepthTexture(bool isDepth)
    {
        qualityLevel[currentLevel].supportsCameraDepthTexture = isDepth;
    }
}
