using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] sounds;

    private Sound theme;
    private void Awake()
    {
        instance = this;

        foreach(Sound sound in sounds)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.audioClip;

            sound.audioSource.volume = sound.volume;
            sound.audioSource.pitch = sound.pitch;

            sound.audioSource.loop = sound.loop;
        }
    }

    private void Start()
    {
        StartTheme("GoingHigher");
    }

    public Sound Play(string text)
    {
        Sound sound = Array.Find(sounds, s => s.audioName == text);
        sound?.audioSource.Play();
        return sound;
    }

    public void StartTheme(string theme)
    {
        this.theme = Play(theme); ;
    }

    public void StopTheme()
    {
        theme.audioSource.Stop();
    }
}
