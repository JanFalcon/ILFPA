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
    private List<string> correctAnswers;

    public GameObject buttons;

    private AudioManager audioManager;

    private Coroutine questionError, answerError;

    private void Awake()
    {
        correctAnswers = new List<string>();
    }
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
        correctAnswers.Clear();
        TestPanel();
        string[] value = questionnaire.Split('|');
        question.text = value[0];
        for (int i = 1; i < value.Length; i++)
        {
            correctAnswers.Add(value[i].Trim());
        }
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
        string answer = answerField.text.Replace(" ", "");
        answer = answer.Trim();
        if (laptopScript.Save(questionField.text, answer))
        {
            questionField.text = "";
            answerField.text = "";
            AudioManager.instance.Play("Boop");
            //BackToAdmin();
            return;
        }

        AudioManager.instance.Play("Error");

        if (string.IsNullOrWhiteSpace(questionField.text))
        {
            if (questionError != null)
            {
                StopCoroutine(questionError);
            }
            questionError = StartCoroutine(Error(0.5f, questionField.gameObject.GetComponent<Image>()));
        }
        if (string.IsNullOrWhiteSpace(answerField.text))
        {
            if (answerError != null)
            {
                StopCoroutine(answerError);
            }
            answerError = StartCoroutine(Error(0.5f, answerField.gameObject.GetComponent<Image>()));
        }
    }

    public IEnumerator Error(float seconds, Image image)
    {
        image.color = Color.red;
        yield return new WaitForSeconds(seconds);
        image.color = Color.white;
    }

    public void Clear()
    {
        questionField.text = "";
        answerField.text = "";
    }

    public void CheckAnswer()
    {
        string answerText = answer.text.ToLower().Trim();
        answerText = answerText.Replace(" ", "");
        foreach (string correctAnswer in correctAnswers)
        {
            if (answerText.Contains(correctAnswer.ToLower()) && !string.IsNullOrEmpty(answerText))
            {
                audioManager.Play("Correct");
                laptopScript.AddQuestionCtr();
                laptopScript.CheckAnswer();

                answer.text = "";
                answer.Select();
                answer.ActivateInputField();
                return;
            }
        }

        if (answerError != null)
        {
            StopCoroutine(answerError);
        }
        answerError = StartCoroutine(Error(0.5f, answer.gameObject.GetComponent<Image>()));

        audioManager.Play("Error");
    }

    public void Close()
    {
        audioManager.Play("Boop");
        GameManager.instance.UnInteract();
        laptopScript?.Close();
    }
}
