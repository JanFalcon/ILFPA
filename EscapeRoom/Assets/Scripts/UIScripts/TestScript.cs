using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TestScript : MonoBehaviour
{
    public TMP_InputField questionField, answerField;

    private Image answerFieldImage;
    private Color answerFieldColor;

    public ComputerUIScript computerUI;

    private string question, answer;
    private int number = 0;
    private int tries = 0;

    private AudioManager audioManager;
    private void Awake()
    {
        GetProperties();
    }

    private void Start()
    {
        audioManager = AudioManager.instance;
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

        questionField.text = question;
    }

    public void Check()
    {
        tries++;
        string answerText = answerField.text.ToLower();
        if (answerText.Contains(answer) && !string.IsNullOrEmpty(answerText))
        {
            audioManager.Play("Correct");

            computerUI.AddAnsweredQuestions($"{FuzzyLogic.instance.GetDifficulty().ToString()}|{question} / {answer}|{computerUI.GetTime(true)} s|{tries}");

            computerUI.CheckAnswer(number, tries);
            answerField.text = "";
            answerField.Select();
            answerField.ActivateInputField();
        }
        else
        {
            audioManager.Play("Error");
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
