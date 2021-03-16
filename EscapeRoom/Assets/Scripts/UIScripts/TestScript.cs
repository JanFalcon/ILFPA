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

    private string question;
    private List<string> correctAnswers;
    private int number = 0;
    private int tries = 0;

    private AudioManager audioManager;
    private void Awake()
    {
        correctAnswers = new List<string>();
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
        correctAnswers.Clear();
        for (int i = 2; i < value.Length; i++)
        {
            correctAnswers.Add(value[i].Replace(" ", ""));
        }
        tries = 0;

        questionField.text = question;
    }

    public void Check()
    {
        tries++;
        string answerText = answerField.text.Replace(" ", "").ToLower();
        foreach (string correctAnswer in correctAnswers)
        {
            if (answerText.Contains(correctAnswer) && !string.IsNullOrEmpty(answerText))
            {
                audioManager.Play("Correct");

                computerUI.AddAnsweredQuestions($"{FuzzyLogic.instance.GetDifficulty().ToString()}|{question} / {string.Join(", ", correctAnswers.ToArray())}|{computerUI.GetTime(true)} s|{tries}");

                computerUI.CheckAnswer(number, tries);
                answerField.text = "";
                answerField.Select();
                answerField.ActivateInputField();

                return;
            }
        }
        audioManager.Play("Error");
        StartCoroutine(WrongAnswer());
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
