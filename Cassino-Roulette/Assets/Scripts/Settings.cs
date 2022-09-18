using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public AudioMixer mixer;
    public List<GameObject> muteImg;
    public Slider vol;
    public PlayerBalance balance;

    bool muted;

    private void Start()
    {
        muted = false;
    }

    public void Mute()
    {
        if (!muted)
        {
            mixer.SetFloat("Volume", -80f);
            muteImg[0].SetActive(false);
            muteImg[1].SetActive(true);
            muted = true;
        }
        else
        {
            mixer.SetFloat("Volume", 0f);
            muteImg[0].SetActive(true);
            muteImg[1].SetActive(false);
            muted = false;
        }
    }

    public void ChangeVolume()
    {
        mixer.SetFloat("Volume", Mathf.Log(vol.value)*20f);
    }

    public void Loan()
    {
        balance.balance += 1000;
    }
}
