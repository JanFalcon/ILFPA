using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class Game3Script : MonoBehaviour, IInteractable, ISaveable
{
    public GameObject game3UIPrefab;
    private GameObject game3UI;
    private Game3UIScript game3UIScript;
    private Transform canvas;

    private bool finish = false;
    private string hiddenMessage;

    private HighlightScript highLightScript;

    private void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
        Random.InitState(DateTime.Now.Millisecond);
    }

    private void Start()
    {
        GetHighLightScript();
    }

    private void GetHighLightScript()
    {
        highLightScript = GetComponent<HighlightScript>();
    }

    public string GetRandomNumbers()
    {
        string randomValues = "";
        for (int i = 0; i < 10; i++)
        {
            randomValues += Random.Range(0, 10);
        }
        return randomValues;
    }

    public bool Save(string text)
    {
        hiddenMessage = text;
        return true;
    }

    public void SetFinish(bool finish)
    {
        this.finish = finish;
    }

    //Interfaces

    //*IInteractable

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

    public void Interact()
    {
        game3UI = Instantiate(game3UIPrefab, canvas);
        game3UIScript = game3UI.GetComponent<Game3UIScript>();
        game3UIScript?.Open(this, hiddenMessage, finish);
    }
    public void Close()
    {
        PlayerInteract.instance.Close();
        Destroy(game3UI);
    }

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

    [Serializable]
    public struct SaveData
    {
        public string hiddenMessage;
    }
}
