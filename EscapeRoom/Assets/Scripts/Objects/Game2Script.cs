using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game2Script : MonoBehaviour, IInteractable, ISaveable
{
    public GameObject game2Panel;
    private GameObject game2;
    private OneToTenScript game2Script;
    private Transform canvas;
    private string text = "";

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
        this.text = text;
        return true;
    }

    //Interfaces
    //IInteractab;e
    public void Interact()
    {
        game2 = Instantiate(game2Panel, canvas);
        game2Script = game2.GetComponent<OneToTenScript>();
        game2Script?.Open(this, text);
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
        Destroy(game2);
    }

    //ISaveable

    public object CaptureState()
    {
        return new SaveData
        {
            text = this.text,
        };
    }

    public void LoadState(object state)
    {
        SaveData saveData = (SaveData)state;
        this.text = saveData.text;

    }

    [System.Serializable]
    public struct SaveData
    {
        public string text;
    }
}
