using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class OneToTenScript : MonoBehaviour
{
    private readonly int[] numbers = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

    public Transform content;
    public GameObject number;

    private bool finish = false;
    private int counter = 0;

    //To be removed
    private void Start()
    {
        CreateTask();
    }

    public void Open()
    {
        if (!finish)
        {
            CreateTask();
        }
        else
        {
            Finish();
        }
    }

    public void CreateTask()
    {
        counter = 0;
        for(int i = 0; i < content.childCount; i++)
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

    }

    public void Close()
    {
        Destroy(gameObject);
    }

}
