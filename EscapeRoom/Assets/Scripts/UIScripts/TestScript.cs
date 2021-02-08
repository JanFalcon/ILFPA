using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TestScript : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TMP_InputField answerField;

    public ComputerUIScript computerUI;

    private string question, answer;
    private int number = 0;
    private int tries = 0;

    public void SetQuestionAndAnswer(string questionnaire, int number)
    {
        string[] value = questionnaire.Split('|');
        this.number = number;
        question = value[1];
        answer = value[2];
        tries = 0;

        text.text = question;
    }

    public void Check()
    {
        tries++;
        if (answerField.text.Contains(answer))
        {
            //computerUI.Delete(number);
            computerUI.CheckAnswer(number, tries);
            Debug.Log("Correct");
        }

        //if(string.Equals(answerField.text, answer))
        //{
        //    //computerUI.Delete(number);
        //    computerUI.CheckAnswer(number, tries);
        //    Debug.Log("Correct");
        //}
        //Add wrong UI
    }

    public void Back()
    {
        if (GameManager.instance.GetCreatorMode())
        {
            computerUI.BackToAdmin();
        }
        else
        {
            computerUI.Close();
        }

    }
}
