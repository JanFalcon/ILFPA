using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class LaptopUIScript : MonoBehaviour
{
    private LaptopScript laptopScript;

    public GameObject adminPanel, questionPanel, questionViewerPanel, testPanel;
    public GameObject questionViewerContents, questionViewPort;

    public TMP_InputField questionField, answerField;

    public TMP_InputField question, answer;
    private string correctAnswer;

    public GameObject buttons;

    private AudioManager audioManager;
    private void Start()
    {
        audioManager = AudioManager.instance;
    }

    public void CloseAll()
    {
        testPanel.SetActive(false);
        adminPanel.SetActive(false);
        questionPanel.SetActive(false);
        questionViewerPanel.SetActive(false);
    }

    public void BackToMenu()
    {
        CloseAll();
        adminPanel.SetActive(true);
    }

    public void AddAQuestion()
    {
        CloseAll();
        questionPanel.SetActive(true);
    }

    public void QuestionViewer()
    {
        CloseAll();
        questionViewerPanel.SetActive(true);
        SetQuestionViewer();
    }

    public void TestPanel()
    {
        CloseAll();
        testPanel.SetActive(true);
    }

    public void SetText(string questionnaire)
    {
        TestPanel();
        string[] value = questionnaire.Split('|');
        question.text = value[0];
        correctAnswer = value[1];
    }

    public void SetLaptopScript(LaptopScript laptopScript)
    {
        if (!GameManager.instance.GetCreatorMode())
        {
            buttons.SetActive(false);
            TestPanel();
        }
        gameObject.SetActive(true);
        BackToMenu();
        this.laptopScript = laptopScript;
    }

    public void SetQuestionViewer()
    {
        for (int i = 0; i < questionViewPort.transform.childCount; i++)
        {
            Destroy(questionViewPort.transform.GetChild(i).gameObject);
        }

        int ctr = 0;
        foreach (string question in laptopScript.GetQuestions())
        {
            Instantiate(questionViewerContents, questionViewPort.transform).GetComponent<QuestionViewer2>().SetText(question, ctr, this);
            ctr++;
        }
    }

    public bool Delete(string text)
    {
        return laptopScript.Delete(text);
    }

    public void Save()
    {
        if (laptopScript.Save(questionField.text, answerField.text))
        {
            questionField.text = "";
            answerField.text = "";
        }
        //BackToAdmin();
    }

    public void Clear()
    {
        questionField.text = "";
        answerField.text = "";
    }

    public void CheckAnswer()
    {
        correctAnswer = correctAnswer.ToLower();
        string answerText = answer.text.ToLower();
        if (answerText.Contains(correctAnswer) && !string.IsNullOrEmpty(answerText))
        {
            audioManager.Play("Correct");
            laptopScript.AddQuestionCtr();
            laptopScript.CheckAnswer();

            answer.text = "";
            answer.Select();
            answer.ActivateInputField();

        }
        else
        {
            audioManager.Play("Error");
        }
    }

    public void Close()
    {
        audioManager.Play("Boop");
        GameManager.instance.UnInteract();
        laptopScript?.Close();
    }
}
