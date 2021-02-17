﻿using System.Collections;
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

    private bool once = false;

    private void Start()
    {
        if (!GameManager.instance.GetCreatorMode())
        {
            CloseAll();
            TestPanel();
        }
        else
        {
            BackToAdmin();
        }
    }

    public void Open()
    {
        gameObject.SetActive(true);
        GameManager.instance.Interact();

        if (!GameManager.instance.GetCreatorMode())
        {
            CloseAll();
            TestPanel();
        }

        if (!once)
        {
            once = true;
            computerScript.ResetTimer();
        }
    }

    public void SetOnce()
    {
        once = false;
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
        //float easyValue = sliderScript.easySlider.value / 100f;
        //float mediumValue = sliderScript.mediumSlider.value / 100f;
        //float hardValue = sliderScript.hardSlider.value / 100f;
        //float min = Mathf.Min(Mathf.Min(CheckValue(easyValue), CheckValue(mediumValue)), CheckValue(hardValue));

        //Debug.Log(sliderScript.CheckDifficulty().ToString());
        //Debug.Log($"MIN : {min}");

        //float value = FuzzyLogic.instance.CalculateTime(min, sliderScript.CheckDifficulty());

        //if(computerScript.Save(value, questionInputField.text, answerInputField.text))
        //{
        //    questionInputField.text = "";
        //    answerInputField.text = "";
        //}
        //sliderScript.Reset();
        int difficulty = ButtonRatingScript.instance.GetDifficulty();
        float value = 0f;
        switch (difficulty)
        {
            case 1:
                //0
                value = 0f;
                break;
            case 2:
                //25
                value = 25f;
                break;
            case 3:
                //50
                value = 50f;
                break;
            case 4:
                //75
                value = 75f;
                break;
            case 5:
                //100
                value = 100f;
                break;
            default:
                Debug.Log($"NO Such thing... => {difficulty}");
                break;
        }

        if (computerScript.Save(value, questionInputField.text, answerInputField.text))
        {
            questionInputField.text = "";
            answerInputField.text = "";
        }
        ButtonRatingScript.instance.ResetValues();
        AudioManager.instance.Play("Boop");
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
            computerScript.onTimer = false;
            //ADD UI (CORRECT)
            string difficulty = FuzzyLogic.instance.GetDifficulty();

            //Set New Values
            computerScript.SetValues(computerScript.timer, (float)tries);
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
            evaluationScript.SetEvaluation($"Difficulty : {difficulty}\n{value}\nMax Value : {FuzzyLogic.instance.GetMax()}\nFuzzy Value : {FuzzyLogic.instance.GetFuzzyValue()}");
        }
    }

    public void AddAnsweredQuestions(string text)
    {
        computerScript.AddAnsweredQuestions(text);
    }

    public string GetTime(bool stop)
    {
        return computerScript.GetTime(stop);
    }

    public void TestPanel()
    {
        CloseAll();
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

    public bool Delete(string text)
    {
        return computerScript.Delete(text);
    }

    public void CloseAll()
    {
        adminContents.SetActive(false);
        questionContents.SetActive(false);
        questionViewer.SetActive(false);
        testPanel.SetActive(false);
        evaluationPanel.SetActive(false);
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
        GameManager.instance.UnInteract();
        gameObject.SetActive(false);
        computerScript.Close();
    }
}
