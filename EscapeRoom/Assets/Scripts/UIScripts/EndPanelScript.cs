using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndPanelScript : MonoBehaviour
{
    public static EndPanelScript instance;
    public GameObject evaluation;
    public Transform contents;
    public ComputerScript computerScript;

    public TextMeshProUGUI title;
    public TextMeshProUGUI totalTime;

    private string playerData = "";

    private void Awake()
    {
        playerData = "";
        instance = this;
    }

    public string GetValues()
    {
        totalTime.text = $"Total Time : {GameManager.instance.timerText.text.Replace("Timer :", "")}";

        playerData = totalTime.text + "\n";
        int ctr = 0;

        foreach (string value in computerScript.GetAnsweredQuestions())
        {
            string[] values = value.Split('|');
            SetValue(values[0], values[1], values[2], values[3], ctr);
            ctr++;
        }

        return playerData;
    }

    public void SetValue(string difficulty, string question, string time, string tries, int number)
    {
        GameObject temp = Instantiate(evaluation, contents) as GameObject;
        playerData += temp.GetComponent<EvalViewer>().SetText(difficulty, question, time, tries, number);
    }

}
