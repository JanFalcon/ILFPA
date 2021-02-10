using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TestScript : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TMP_InputField answerField;

    private Image answerFieldImage;
    private Color answerFieldColor;

    public ComputerUIScript computerUI;

    private string question, answer;
    private int number = 0;
    private int tries = 0;

    private void Awake()
    {
        GetProperties();
    }

    public void GetProperties()
    {
        answerFieldImage = answerField.GetComponent<Image>();
        answerFieldColor = answerFieldImage.color;
    }

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
            answerField.text = "";
        }
        else
        {
            StartCoroutine(WrongAnswer());
        }
    }

    private IEnumerator WrongAnswer()
    {
        if (!answerFieldImage)
        {
            GetProperties();
        }
        answerFieldImage.color = Color.red;
        yield return new WaitForSeconds(0.9f);
        answerFieldImage.color = answerFieldColor;
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
