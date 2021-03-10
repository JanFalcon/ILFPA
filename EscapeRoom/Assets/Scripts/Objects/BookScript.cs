using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[RequireComponent(typeof(HighlightScript))]
public class BookScript : MonoBehaviour, IInteractable, ISaveable
{
    public GameObject spawnItem;

    private string textBody = "";
    private GameObject bookUI;
    private Transform canvas;

    private HighlightScript highLightScript;
    private void Awake()
    {
        GetHighLightScript();
        canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
    }

    private void GetHighLightScript()
    {
        highLightScript = GetComponent<HighlightScript>();
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
            Highlight(highlight);
        }
    }

    public string GetTextBody()
    {
        return textBody;
    }

    public void SetTextBody(string textBody)
    {
        this.textBody = textBody;
    }

    public void Interact()
    {
        //Instantiate UI
        bookUI = Instantiate(spawnItem, canvas);
        BookUIScript bookUIScript = bookUI.GetComponent<BookUIScript>();
        bookUIScript.SetBookScript(this);
        bookUIScript.SetText(textBody);
    }

    public void Close()
    {
        if (bookUI)
        {
            Destroy(bookUI);
        }
        PlayerInteract.instance.Close();
    }

    public object CaptureState()
    {
        return new SaveData
        {
            textBody = this.textBody
        };
    }

    public void LoadState(object state)
    {
        SaveData saveData = (SaveData)state;
        textBody = saveData.textBody;
    }

    [Serializable]
    private struct SaveData
    {
        public string textBody;
    }
}
