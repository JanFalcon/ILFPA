using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game4Script : MonoBehaviour, IInteractable, ISaveable
{
    public GameObject game4UIPrefab;
    private GameObject game4UI;
    private Game4UIScript game4UIScript;
    private Transform canvas;

    private string hiddenMessage;

    private HighlightScript highlightScript;

    private void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
        GetHighlightScript();
    }

    public void GetHighlightScript()
    {
        highlightScript = GetComponent<HighlightScript>();
    }

    public void Save(string hiddenMessage)
    {
        this.hiddenMessage = hiddenMessage;
    }

    //Interfaces

    //*IInteractable

    public void Highlight(bool highlight)
    {
        if (highlightScript)
        {
            highlightScript.Highlight(highlight);
        }
        else
        {
            GetHighlightScript();
        }
    }

    public void Interact()
    {
        game4UI = Instantiate(game4UIPrefab, canvas);
        game4UIScript = game4UI.GetComponent<Game4UIScript>();
        game4UIScript?.Open(this, hiddenMessage);
    }

    public void Close()
    {
        PlayerInteract.instance.Close();
        if (game4UI)
        {
            Destroy(game4UI);
        }
    }

    //*ISaveable

    public object CaptureState()
    {
        return new SaveData
        {
            hiddenMessage = this.hiddenMessage,
        };
    }

    public void LoadState(object state)
    {
        SaveData saveData = (SaveData)state;
        this.hiddenMessage = saveData.hiddenMessage;
    }

    [System.Serializable]
    public struct SaveData
    {
        public string hiddenMessage;
    }
}
