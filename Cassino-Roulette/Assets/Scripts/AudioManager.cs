using UnityEngine;
using System.Collections;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] sounds;

    [HideInInspector]
    public bool canPlay;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach( var sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();

            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            sound.source.mute = sound.mute;
            sound.source.outputAudioMixerGroup = sound.audioMixerGroup;
        }

        canPlay = true;

        Play("Background");
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if(s != null)
            s.source.PlayOneShot(s.source.clip);
    }

    public IEnumerator Wait(float time)
    {
        Play("OneSpin");

        canPlay = false;
        yield return new WaitForSeconds(time);
        canPlay = true;
    }
}
