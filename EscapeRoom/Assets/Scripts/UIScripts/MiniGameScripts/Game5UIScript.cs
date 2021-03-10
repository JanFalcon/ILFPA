using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game5UIScript : MonoBehaviour
{
    public Color bgColor;
    public TMP_InputField secretField, hiddenField;
    public GameObject encodeButton, decodeButton;
    public TMP_InputField inputField;
    public GameObject buttons;
    private const int corrector = 96;
    private string encodedValue, decodedValue;
    private Coroutine codeError;
    private string hiddenMessage;
    private Game5Script game5Script;
    public void Open(Game5Script game5Script, string hiddenMessage, string decodedValue)
    {
        this.game5Script = game5Script;
        this.hiddenMessage = hiddenMessage;

        secretField.text = decodedValue;
        hiddenField.text = hiddenMessage;

        Encode();

        if (!GameManager.instance.GetCreatorMode())
        {
            encodeButton.SetActive(false);
            decodeButton.SetActive(false);
            buttons.SetActive(false);

            secretField.interactable = false;
            hiddenField.interactable = false;
            hiddenField.text = " ";
            hiddenField.GetComponent<Image>().color = bgColor;
        }
    }

    public void Check()
    {
        if (inputField.text.Equals(decodedValue))
        {
            AudioManager.instance.Play("Correct");
            inputField.interactable = false;
            inputField.GetComponent<Image>().color = Color.green;
            hiddenField.text = hiddenMessage;
            return;
        }
        AudioManager.instance.Play("Error");
        StartCoroutine(ErrorEncoding(inputField));
    }

    public void Encode()
    {
        secretField.text = ConvertValues(secretField.text);
    }

    public void Decode()
    {
        secretField.text = ReverseConversion(secretField.text);
    }

    public string ConvertValues(string values)
    {
        //Check if not alphabet
        if (!IsAllLetters(values))
        {
            StartCoroutine(ErrorEncoding(secretField));
            return values;
        }

        values = values.ToLower();
        decodedValue = values;

        char[] chars = values.ToCharArray();
        string s = "";

        int i = 0;
        foreach (char c in chars)
        {
            s += $"{(int)c - corrector}";
            i++;
            if (i < chars.Length)
            {
                s += ", ";
            }
        }
        encodedValue = s;
        return s;
    }

    public string ReverseConversion(string values)
    {
        string check = values.Replace(", ", "");
        if (!IsAllDigits(check))
        {
            StartCoroutine(ErrorEncoding(secretField));
            return values;
        }
        values = values.Replace(" ", "");
        string[] newValues = values.Split(',');

        string s = "";
        foreach (string value in newValues)
        {
            int val = int.Parse(value);

            if (val < 1 || val > 26)
            {
                return encodedValue ?? values;
            }

            s += (char)(val + corrector);
        }
        return s;
    }

    private bool IsAllLetters(string s)
    {
        foreach (char c in s)
        {
            if (!char.IsLetter(c))
            {
                return false;
            }
        }
        return true;
    }

    private bool IsAllDigits(string s)
    {
        foreach (char c in s)
        {
            if (!char.IsDigit(c))
            {
                return false;
            }
        }
        return true;
    }

    private IEnumerator ErrorEncoding(TMP_InputField thisInputField)
    {
        thisInputField.GetComponent<Image>().color = Color.red;
        // inputField.text = "<color=#FF0000>meow</color>";
        yield return new WaitForSeconds(0.5f);
        thisInputField.GetComponent<Image>().color = Color.white;
    }

    public void Save()
    {
        Encode();
        hiddenMessage = hiddenField.text;
        game5Script?.Save(hiddenMessage, decodedValue);
        Close();
    }

    public void Clear()
    {
        hiddenField.text = "";
    }

    public void Close()
    {
        AudioManager.instance.Play("Boop");
        GameManager.instance.UnInteract();
        game5Script?.Close();
    }
}
