using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game5Script : MonoBehaviour, IInteractable, ISaveable
{
    public GameObject game5UIPrefab;
    private GameObject game5UI;
    private Game5UIScript game5UIScript;
    private Transform canvas;

    private string hiddenMessage, decodedValue;
    private HighlightScript highlightScript;

    private void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
        highlightScript = GetComponent<HighlightScript>();
    }

    public void Save(string hiddenMessage, string decodedValue)
    {
        this.hiddenMessage = hiddenMessage;
        this.decodedValue = decodedValue;
    }

    //Interfaces
    //*IInteractable
    public void Highlight(bool highlight)
    {
        if (highlightScript)
        {
            highlightScript.Highlight(highlight);
        }
    }

    public void Interact()
    {
        game5UI = Instantiate(game5UIPrefab, canvas);
        game5UIScript = game5UI.GetComponent<Game5UIScript>();
        game5UIScript.Open(this, hiddenMessage, decodedValue);
    }

    public void Close()
    {
        PlayerInteract.instance.Close();
        if (game5UI)
        {
            Destroy(game5UI);
        }
    }

    //*ISaveable
    public object CaptureState()
    {
        return new SaveData
        {
            decodedValue = this.decodedValue,
            hiddenMessage = this.hiddenMessage,
        };
    }

    public void LoadState(object state)
    {
        SaveData saveData = (SaveData)state;
        this.decodedValue = saveData.decodedValue;
        this.hiddenMessage = saveData.hiddenMessage;
    }

    [System.Serializable]
    public struct SaveData
    {
        public string decodedValue;
        public string hiddenMessage;
    }
}
