using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;
using Random = UnityEngine.Random;

public class Game4UIScript : MonoBehaviour
{

    public TMP_InputField inputField;
    public GameObject buttonPrefab;
    public GameObject message;
    public GameObject buttons;
    private Transform parent;

    private Game4Button firstValue, secondValue;
    private bool working = false;
    private int[] values = { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6 };

    private int counter = 0;
    private Game4Script game4Script;
    private void Awake()
    {
        parent = transform.GetChild(0).transform;
    }

    public void Open(Game4Script game4Script, string hiddenMessage)
    {
        this.game4Script = game4Script;
        inputField.text = hiddenMessage;

        int[] newValues = FisherShuffle(values);

        foreach (int value in newValues)
        {
            Instantiate(buttonPrefab, parent).GetComponent<Game4Button>().SetValue(value, this);
        }

        StartCoroutine(CloseLayout());
    }

    private IEnumerator CloseLayout()
    {
        if (GameManager.instance.GetCreatorMode())
        {
            message.SetActive(true);
        }
        else
        {
            buttons.SetActive(false);
        }

        yield return new WaitForSeconds(0.1f);
        transform.GetChild(0).GetComponent<GridLayoutGroup>().enabled = false;

        if (GameManager.instance.GetCreatorMode())
        {
            parent.gameObject.SetActive(false);
        }
    }

    public int[] FisherShuffle(int[] values)
    {
        Random.InitState(DateTime.Now.Millisecond);
        int[] numbers = values;
        for (int i = 0; i < numbers.Length; i++)
        {
            int random = Random.Range(0, numbers.Length);

            int temp = numbers[i];
            int temp2 = numbers[random];

            numbers[i] = temp2;
            numbers[random] = temp;
        }
        return numbers;
    }

    public void Compare(Game4Button value)
    {
        if (working)
        {
            return;
        }

        if (firstValue)
        {
            working = true;
            secondValue = value;
            secondValue.HighLight();

            if (firstValue == secondValue)
            {
                firstValue.RemoveHighLight();
                secondValue.RemoveHighLight();
                firstValue = null;
                secondValue = null;

                working = false;
                AudioManager.instance.Play("Boop");


                return;
            }

            if (firstValue.GetValue() != secondValue.GetValue())
            {
                StartCoroutine(WrongAnswer());
                return;
            }


            StartCoroutine(CorrectAnswer());
            return;
        }

        AudioManager.instance.Play("Boop");
        firstValue = value;
        firstValue.HighLight();
    }


    private IEnumerator CorrectAnswer()
    {
        AudioManager.instance.Play("Correct");
        firstValue.CorrectAnswer();
        secondValue.CorrectAnswer();

        yield return new WaitForSeconds(0.5f);

        firstValue.DestroyThis();
        secondValue.DestroyThis();
        firstValue = null;
        secondValue = null;
        working = false;

        counter++;

        if (counter == 6)
        {
            parent.gameObject.SetActive(false);
            message.SetActive(true);
        }
    }

    private IEnumerator WrongAnswer()
    {
        AudioManager.instance.Play("Error");
        firstValue.WrongAnswer();
        secondValue.WrongAnswer();

        yield return new WaitForSeconds(0.5f);

        firstValue.RemoveHighLight();
        secondValue.RemoveHighLight();

        firstValue = null;
        secondValue = null;
        working = false;
    }

    public void Save()
    {
        game4Script?.Save(inputField.text);
        Close();
    }

    public void Swap()
    {
        AudioManager.instance.Play("Boop");
        message.SetActive(!message.activeSelf);
        parent.gameObject.SetActive(!parent.gameObject.activeSelf);
    }

    public void Clear()
    {
        inputField.text = "";
    }

    public void Close()
    {
        AudioManager.instance.Play("Boop");
        GameManager.instance.UnInteract();
        game4Script?.Close();
    }
}
