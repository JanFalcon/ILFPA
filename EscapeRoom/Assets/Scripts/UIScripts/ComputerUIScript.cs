using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ComputerUIScript : MonoBehaviour
{
    public GameObject adminContents;
    public GameObject questionContents;
    public GameObject questionViewer;

    public GameObject questionViewerViewPort;
    public GameObject questionViewerContents;

    public GameObject testPanel;
    public GameObject evaluationPanel;

    public ComputerScript computerScript;
    public TestScript testScript;
    public EvaluationScript evaluationScript;

    public TMP_InputField questionInputField;
    public TMP_InputField answerInputField;

    public SliderScript sliderScript;

    private bool once = false;


    private void Start()
    {
        BackToAdmin();
    }

    public void Open()
    {
        gameObject.SetActive(true);

        if (!once)
        {
            once = true;
            GameManager.instance.ResetTime();
        }
    }

    public void GetQuestionContents()
    {
        adminContents.SetActive(false);
        questionViewer.SetActive(false);
        questionContents.SetActive(true);
    }

    public void GetQuestionViewer()
    {
        adminContents.SetActive(false);
        questionContents.SetActive(false);
        questionViewer.SetActive(true);

        SetQuestionViewer();
    }

    public void Save()
    {
        float easyValue = sliderScript.easySlider.value / 100f;
        float mediumValue = sliderScript.mediumSlider.value / 100f;
        float hardValue = sliderScript.hardSlider.value / 100f;
        float min = Mathf.Min(Mathf.Min(CheckValue(easyValue), CheckValue(mediumValue)), CheckValue(hardValue));

        Debug.Log(sliderScript.CheckDifficulty().ToString());
        Debug.Log($"MIN : {min}");
        float value = FuzzyLogic.instance.CalculateTime(min, sliderScript.CheckDifficulty());

        if(computerScript.Save(value, questionInputField.text, answerInputField.text))
        {
            questionInputField.text = "";
            answerInputField.text = "";
        }
        BackToAdmin();
    }

    public float CheckValue(float value)
    {
        return value > 0 ? value : 1;
    }

    public void SetQuestionViewer()
    {
        for(int i = 0; i < questionViewerViewPort.transform.childCount; i++)
        {
            Destroy(questionViewerViewPort.transform.GetChild(i).gameObject);
        }

        int ctr = 0;
        foreach(string question in computerScript.GetQuestions())
        {
            Instantiate(questionViewerContents, questionViewerViewPort.transform).GetComponent<QuestionViewer>().SetText(question, ctr, this);
            ctr++;
        }
    }

    public void CheckAnswer(int number, int tries)
    {
        if (computerScript.Delete(number))
        {
            //ADD UI (CORRECT)
            //Set New Values
            computerScript.SetValues(GameManager.instance.GetTimer(), (float)tries);
            //Get Next QuestionNumber
            int nextQuestionNumber = computerScript.GetQuestionNumber();

            //Evaluation
            string[] acceptedRules = FuzzyLogic.instance.GetAcceptedRules();
            string value = "";
            foreach(string rules in acceptedRules)
            {
                value += $"{rules}\n";
            }

            testPanel.SetActive(false);
            evaluationPanel.SetActive(true);
            evaluationScript.SetEvaluation($"Difficulty : {FuzzyLogic.instance.GetDifficulty()}\n{value}\nMax Value : {FuzzyLogic.instance.GetMax()}\nFuzzy Value : {FuzzyLogic.instance.GetFuzzyValue()}");

            //testScript.SetQuestionAndAnswer(computerScript.GetQuestion(nextQuestionNumber), nextQuestionNumber);
        }
    }

    public void TestPanel()
    {
        //Activate
        BackToAdmin();
        adminContents.SetActive(false);
        testPanel.SetActive(true);

        if (computerScript.TestQuestions())
        {
            int nextQuestionNumber = computerScript.GetQuestionNumber();
            testScript.SetQuestionAndAnswer(computerScript.GetQuestion(nextQuestionNumber), nextQuestionNumber);
        }

    }

    public bool Delete(int number)
    {
        return computerScript.Delete(number);
    }

    public void BackToAdmin()
    {
        questionContents.SetActive(false);
        questionViewer.SetActive(false);
        testPanel.SetActive(false);
        evaluationPanel.SetActive(false);
        adminContents.SetActive(true);
    }

    public void Close()
    {
        BackToAdmin();
        gameObject.SetActive(false);
        computerScript.Close();
    }
}
