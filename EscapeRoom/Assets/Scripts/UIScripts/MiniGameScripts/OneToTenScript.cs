using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

public class OneToTenScript : MonoBehaviour
{
    private readonly int[] numbers = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

    public Transform content;
    public GameObject number;
    public GameObject admin;
    public GameObject buttons;
    public TMP_InputField inputField;
    private bool finish = false;
    private int counter = 0;
    private Game2Script game2Script;
    public void Open(Game2Script game2Script, string text)
    {
        this.game2Script = game2Script;

        if (GameManager.instance.GetCreatorMode())
        {
            CreatorMode(true, text);
            return;
        }

        CreatorMode(false, text);
        if (!finish)
        {
            AudioManager.instance.Play("Boop");
            CreateTask();
        }
        else
        {
            AudioManager.instance.Play("Correct");
            Finish();
        }
    }

    public void CreatorMode(bool creator, string text)
    {
        //SHOW UI
        admin?.SetActive(creator);
        buttons?.SetActive(creator);

        inputField.interactable = creator;
        inputField.text = text;
    }

    public void Save()
    {
        if (game2Script.Save(inputField.text))
        {
            AudioManager.instance.Play("Boop");
            Close();
        }
    }

    public void Swap()
    {
        admin?.SetActive(!admin.activeSelf);
        CreateTask();
    }

    public void Clear()
    {
        inputField.text = "";
    }

    public void CreateTask()
    {
        counter = 0;
        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }

        foreach (int number in FisherShuffle(numbers))
        {
            Instantiate(this.number, content).GetComponent<NumberScript>().SetNumber(number, this);
        }
    }

    // O(n) algorithm for shuflling :)
    public int[] FisherShuffle(int[] values)
    {
        int[] numbers = values;
        for (int i = 0; i < numbers.Length; i++)
        {
            Random.InitState(DateTime.Now.Millisecond);
            int random = Random.Range(0, values.Length);

            int temp = numbers[i];
            int temp2 = numbers[random];

            numbers[i] = temp2;
            numbers[random] = temp;
        }
        return numbers;
    }

    public bool CheckCounter(int number)
    {
        if (number != counter)
        {
            AudioManager.instance.Play("Error");
            StartCoroutine(ChangeValues());
            return false;
        }

        counter++;

        if (counter != 10)
        {
            AudioManager.instance.Play("Boop");
        }
        else
        {
            AudioManager.instance.Play("Correct");
            finish = true;
            Finish();
        }
        return true;
        //Close
    }

    private IEnumerator ChangeValues()
    {
        yield return new WaitForSeconds(1.5f);
        CreateTask();
    }

    public void Finish()
    {
        finish = true;
        admin.SetActive(true);
    }

    public void Close()
    {
        game2Script?.Close();
        Destroy(gameObject);
    }

}
