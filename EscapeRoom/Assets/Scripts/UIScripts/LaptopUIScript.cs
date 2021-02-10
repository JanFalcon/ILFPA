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

    public TextMeshProUGUI question;
    public TMP_InputField answer;
    private string correctAnswer;

    public GameObject buttons;

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
        this.question.text = value[0];
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
        GameManager.instance.Interact();
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
        if(laptopScript.Save(questionField.text, answerField.text))
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
        if (correctAnswer.Contains(answer.text))
        {
            laptopScript.AddQuestionCtr();
            laptopScript.CheckAnswer();
        }
    }

    public void Close()
    {
        GameManager.instance.UnInteract();
        laptopScript?.Close();
    }
}
