using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampScript : MonoBehaviour, IInteractable, ISaveable
{
    public GameObject lampLight;
    private bool lampSwitch = false;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Close()
    {

    }

    public void Highlight(bool highlight)
    {

    }

    public void Interact()
    {
        audioSource?.Play();
        lampSwitch = !lampSwitch;
        lampLight.SetActive(lampSwitch);
        PlayerMovementScript.instance.enabled = true;
    }

    public object CaptureState()
    {
        return new SaveData
        {
            lampSwitch = this.lampSwitch,
        };
    }

    public void LoadState(object state)
    {
        SaveData saveData = (SaveData)state;
        lampSwitch = saveData.lampSwitch;
        lampLight.SetActive(lampSwitch);
    }

    [System.Serializable]
    public struct SaveData
    {
        public bool lampSwitch;
    }
}
