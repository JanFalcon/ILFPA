using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPanelScript : MonoBehaviour
{
    public static EndPanelScript instance;

    public GameObject evaluation;
    public Transform contents;
    public ComputerScript computerScript;

    private void Awake()
    {
        instance = this;
    }

    public void GetValues()
    {
        int ctr = 0;
        foreach(string value in computerScript.GetAnsweredQuestions())
        {
            string[] values = value.Split('|');
            SetValue(values[0], values[1], values[2], values[3], ctr);
            ctr++;
        }
    }

    public void SetValue(string difficulty, string question, string time, string tries, int number)
    {
        GameObject temp = Instantiate(evaluation, contents) as GameObject;
        temp.GetComponent<EvalViewer>().SetText(difficulty, question, time, tries, number);
    }

}
