using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game3UIScript : MonoBehaviour
{
    public TMP_InputField numberField, valueField;

    public TMP_InputField[] randomNumbers;
    public GameObject buttons;

    private Game3Script game3Script;

    private string correctGuess = "";
    private string hiddenMessage;

    private bool creator = false;
    private void Start()
    {
        creator = GameManager.instance.GetCreatorMode();
        buttons.SetActive(creator);
        valueField.interactable = creator;
    }

    public void Open(Game3Script game3Script, string hiddenMessage, bool finish)
    {
        this.game3Script = game3Script;
        this.hiddenMessage = hiddenMessage;

        if (GameManager.instance.GetCreatorMode() || finish)
        {
            valueField.text = hiddenMessage;
        }

        foreach (TMP_InputField inputField in randomNumbers)
        {
            inputField.text = this.game3Script?.GetRandomNumbers();
        }

        PickCorrectValue();
    }

    public void PickCorrectValue()
    {
        correctGuess = randomNumbers[Random.Range(0, randomNumbers.Length)].text;
    }

    public void AddNumber(int number)
    {
        numberField.text += number;
    }

    public void Check()
    {
        if (numberField.text.Equals(correctGuess))
        {
            AudioManager.instance.Play("Correct");
            numberField.GetComponent<Image>().color = Color.green;
            valueField.text = hiddenMessage;

            game3Script?.SetFinish(true);
        }
        else
        {
            foreach (TMP_InputField value in randomNumbers)
            {
                if (numberField.text.Equals(value.text))
                {
                    value.GetComponent<Image>().color = Color.red;
                    break;
                }
            }

            AudioManager.instance.Play("Error");
            StartCoroutine(WrongAnswer());
        }
    }

    private IEnumerator WrongAnswer()
    {
        Color color = numberField.GetComponent<Image>().color;
        numberField.GetComponent<Image>().color = Color.red;
        yield return new WaitForSeconds(2f);
        numberField.GetComponent<Image>().color = color;
        numberField.text = "";
    }

    public void ClearNumber()
    {
        numberField.text = "";
    }

    public void Clear()
    {
        valueField.text = "";
    }

    public void Save()
    {
        if (game3Script.Save(valueField.text))
        {
            AudioManager.instance.Play("Boop");
            Close();
        }
    }

    public void Close()
    {
        game3Script?.Close();
    }
}
