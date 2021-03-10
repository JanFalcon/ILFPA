using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioMixer audioMixer;
    public Sound[] sounds;

    public Slider[] sliders;

    private Sound theme;
    private void Awake()
    {
        instance = this;

        foreach (Sound sound in sounds)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.audioClip;

            sound.audioSource.volume = sound.volume;
            sound.audioSource.pitch = sound.pitch;

            sound.audioSource.loop = sound.loop;
            AddMixerGroup(sound.audioSource, sound.groupName);
        }
    }

    private void Start()
    {
        Random.InitState(System.DateTime.Now.Millisecond);

        if (Random.Range(0, 2) == 0)
        {
            StartTheme("GoingHigher");
        }
        else
        {
            StartTheme("NewBeginning");
        }

        Default();
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

    public void AddMixerGroup(AudioSource source, string group)
    {
        source.outputAudioMixerGroup = audioMixer.FindMatchingGroups(group)[0];
    }

    public void SetMasterVolume(Slider slider)
    {
        PlayerPrefs.SetFloat("Master", slider.value);
        audioMixer.SetFloat("Master", slider.value);
    }

    public void SetPSFXVolume(Slider slider)
    {
        PlayerPrefs.SetFloat("PSFX", slider.value);
        audioMixer.SetFloat("PSFX", slider.value);
    }

    public void SetSFXVolume(Slider slider)
    {
        PlayerPrefs.SetFloat("SFX", slider.value);
        audioMixer.SetFloat("SFX", slider.value);
    }

    public void SetBGMVolume(Slider slider)
    {
        PlayerPrefs.SetFloat("BGM", slider.value);
        audioMixer.SetFloat("BGM", slider.value);
    }

    public void SetAMBVolume(Slider slider)
    {
        PlayerPrefs.SetFloat("AMB", slider.value);
        audioMixer.SetFloat("AMB", slider.value);
    }

    public void Default()
    {
        sliders[0].value = PlayerPrefs.GetFloat("Master", 0f);
        sliders[1].value = PlayerPrefs.GetFloat("PSFX", 15f);
        sliders[2].value = PlayerPrefs.GetFloat("SFX", 1f);
        sliders[3].value = PlayerPrefs.GetFloat("BGM", 0f);
        sliders[4].value = PlayerPrefs.GetFloat("AMB", 15f);
    }

}
