using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BookUIScript : MonoBehaviour
{
    private BookScript bookScript;
    //private 
    public TextMeshProUGUI textValue;
    public TMP_InputField inputField;

    public GameObject buttons;

    private void Start()
    {
        if (!GameManager.instance.GetCreatorMode())
        {
            buttons.SetActive(false);
            inputField.interactable = false;
        }
    }

    public void SetBookScript(BookScript bookScript)
    {
        this.bookScript = bookScript;
    }

    public void SaveText()
    {
        if (bookScript) 
        {
            bookScript.SetTextBody(textValue.text);
            Debug.Log("SAVED");
            //ADD DESIGN
        }
        else
        {
            Debug.Log("No Book");
        }
    }

    public void SetText(string text)
    {
        textValue.text = text;
        inputField.text = text;
    }

    public void ClearText()
    {
        textValue.text = "";
        inputField.text = "";
    }

    public void Close()
    {
        if (bookScript)
        {
            bookScript.Close();
        }
    }
}
