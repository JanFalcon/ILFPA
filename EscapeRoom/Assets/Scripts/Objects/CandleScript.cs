using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CandleScript : MonoBehaviour, IInteractable, ISaveable
{
    public GameObject flames;
    private bool flameOn = false;
    private AudioSource audioSource;
    private HighlightScript highlightScript;
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer[] flamesSpriteRenderers;
    private void Awake()
    {
        highlightScript = GetComponent<HighlightScript>();
        audioSource = GetComponent<AudioSource>();
        audioSource?.Pause();
    }


    void Start()
    {
        AudioManager.instance.AddMixerGroup(audioSource, "SFX");
    }

    public void Close()
    {

    }

    public void Highlight(bool highlight)
    {
        highlightScript?.Highlight(highlight);
    }

    public void Interact()
    {
        TurnOn();

        GameManager.instance.UnInteract();
        PlayerInteract.instance.Close();
    }

    public void TurnOn()
    {
        flameOn = !flameOn;
        flames?.SetActive(flameOn);

        if (flameOn)
        {
            audioSource?.UnPause();
            foreach (SpriteRenderer spriteRend in flamesSpriteRenderers)
            {
                spriteRend.sortingOrder = spriteRenderer.sortingOrder + 10;
            }
        }
        else
        {
            audioSource.Pause();
        }
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

        flameOn = !saveData.flameOn;
        TurnOn();
    }

    [System.Serializable]
    public struct SaveData
    {
        public bool flameOn;
    }
}
