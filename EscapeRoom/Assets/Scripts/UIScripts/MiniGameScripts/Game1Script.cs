using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game1Script : MonoBehaviour, IInteractable, ISaveable
{
    private Transform canvas;
    public GameObject game1UIPrefab;
    private GameObject game1UI;

    private string hiddenMessage;

    private HighlightScript highlightScript;
    private void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
    }

    private void Start()
    {
        GetHighlightScript();
    }

    public void GetHighlightScript()
    {
        highlightScript = GetComponent<HighlightScript>();
    }

    public bool Save(string text)
    {
        hiddenMessage = text;
        return true;
    }

    //Interface
    //* IInteractable
    public void Highlight(bool highlight)
    {
        if (highlightScript)
        {
            highlightScript.Highlight(highlight);
            return;
        }
        GetHighlightScript();
    }

    public void Interact()
    {
        game1UI = Instantiate(game1UIPrefab, canvas);
        game1UI.GetComponent<Game1UIScript>().Open(this, hiddenMessage);
    }

    //* ISaveable
    public void LoadState(object state)
    {
        SaveData saveData = (SaveData)state;
        this.hiddenMessage = saveData.hiddenMessage;
    }

    public object CaptureState()
    {
        return new SaveData
        {
            hiddenMessage = this.hiddenMessage,
        };
    }

    public void Close()
    {
        if (game1UI)
        {
            Destroy(game1UI);
        }
        PlayerInteract.instance.Close();
    }

    [System.Serializable]
    public struct SaveData
    {
        public string hiddenMessage;
    }
}