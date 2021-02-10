using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BookUIScript : MonoBehaviour
{
    private BookScript bookScript;
    //private 
    public TMP_InputField inputField;

    public GameObject buttons;

    public void SetBookScript(BookScript bookScript)
    {
        if (!GameManager.instance.GetCreatorMode())
        {
            buttons.SetActive(false);
            inputField.interactable = false;
        }
        GameManager.instance.Interact();
        this.bookScript = bookScript;
    }

    public void SaveText()
    {
        if (bookScript) 
        {
            bookScript.SetTextBody(inputField.text);
            Close();
        }
        else
        {
            Debug.Log("No Book");
        }
    }

    public void SetText(string text)
    {
        inputField.text = text;
    }

    public void ClearText()
    {
        inputField.text = "";
    }

    public void Close()
    {
        if (bookScript)
        {
            GameManager.instance.UnInteract();
            bookScript.Close();
        }
    }
}
