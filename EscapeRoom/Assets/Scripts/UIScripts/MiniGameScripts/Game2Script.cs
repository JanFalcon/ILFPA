using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game2Script : MonoBehaviour, IInteractable, ISaveable
{
    public GameObject game2Panel;
    private GameObject game2UI;
    private Transform canvas;
    private string hiddenMessage = "";

    private HighlightScript highLightScript;
    private void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
    }

    private void Start()
    {
        GetHighLightScript();
    }

    private void GetHighLightScript()
    {
        highLightScript = GetComponent<HighlightScript>();
    }

    public bool Save(string text)
    {
        this.hiddenMessage = text;
        return true;
    }

    //Interfaces
    //IInteractable
    public void Interact()
    {
        game2UI = Instantiate(game2Panel, canvas);
        game2UI.GetComponent<OneToTenScript>().Open(this, hiddenMessage);
    }

    public void Highlight(bool highlight)
    {
        if (highLightScript)
        {
            highLightScript.Highlight(highlight);
        }
        else
        {
            GetHighLightScript();
        }
    }
    public void Close()
    {
        PlayerInteract.instance.Close();
        Destroy(game2UI);
    }

    //ISaveable

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
