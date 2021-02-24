using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleScript : MonoBehaviour, IInteractable, ISaveable
{
    private GameObject flames;
    private bool flameOn;
    private AudioSource audioSource;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Pause();

        flames = transform.GetChild(0).gameObject;
        flameOn = false;
        flames?.SetActive(flameOn);
    }

    public void Close()
    {
        
    }

    public void Highlight(bool highlight)
    {
    }

    public void Interact()
    {
        flameOn = !flameOn;

        flames?.SetActive(flameOn);

        if (flameOn)
        {
            audioSource.UnPause();
        }
        else
        {
            audioSource.Pause();
        }

        PlayerInteract.instance.Close();
    }


    public object CaptureState()
    {
        return new SaveData
        {
            flameOn = this.flameOn,
        };
    }


    public void LoadState(object state)
    {
        SaveData saveData = (SaveData)state;
        this.flameOn = saveData.flameOn;
        flames?.SetActive(this.flameOn);
    }

    [System.Serializable]
    public struct SaveData
    {
        public bool flameOn;
    }
}
