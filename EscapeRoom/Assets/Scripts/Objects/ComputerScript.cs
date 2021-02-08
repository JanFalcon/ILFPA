﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ComputerScript : MonoBehaviour, IInteractable, ISaveable
{
    private List<string> questionnaires;

    public GameObject computerPanel;
    public ComputerUIScript computerUIScript;

    private FinishRoomScript finishRoom;

    public float time = 1000f;
    public float tries = 1000f;

    private int questionNumber = 0;
    private int questionCounter = 0;

    public float timer = 0;

    private HighlightScript highLightScript;
    private void Awake()
    {
        questionnaires = new List<string>();
    }

    private void Start()
    {
        finishRoom = transform.GetChild(0).GetComponent<FinishRoomScript>();

        computerPanel.SetActive(false);
        GetHighLightScript();
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }

    public void ResetTimer()
    {
        timer = 0;
    }

    private void GetHighLightScript()
    {
        highLightScript = GetComponent<HighlightScript>();
    }

    public bool Save(float value, string question, string answer)
    {
        questionnaires.Add($"{value.ToString()}|{question}|{answer}");
        Debug.Log("Added Successfully");

        return true;
    }

    public void SetValues(float time, float tries)
    {
        this.time = time;
        this.tries = tries;
    }

    public int GetQuestionNumber()
    {
        return CalculateNextQUestion(FuzzyLogic.instance.FuzzyRules(time, tries));
    }

    public string GetQuestion(int number)
    {
        if (questionnaires.Count > 0)
        {
            questionCounter++;
            return questionnaires[number];
        }
        else
        {
            return "NO QUESTIONS IN THE SYSTEM!";
        }
    }

    public int CalculateNextQUestion(float value)
    {
        float[] fuzzyValues = ListQuestions();
        float difference = 1000f;
        //float targetQuestion = 0f;

        int ctr = 0;
        foreach (float fuzzyValue in fuzzyValues)
        {
            float dif = Math.Abs(value - fuzzyValue);
            if (dif < difference)
            {
                difference = dif;
                //targetQuestion = fuzzyValue;
                questionNumber = ctr;
            }

            ctr++;
        }

        //Debug.Log(targetQuestion);
        //return questionnaires[questionNumber];
        return questionNumber;
    }

    public float[] ListQuestions()
    {
        int ctr = 0;
        float[] fuzzyValues = new float[questionnaires.Count];
        foreach (string question in questionnaires)
        {
            fuzzyValues[ctr] = float.Parse(question.Split('|')[0]);
            ctr++;
        }
        //Array.Sort(fuzzyValues);

        return fuzzyValues;
    }

    public void CheckAnswer(int questionNumber, string answer, bool removeMe)
    {
        string correctAnswer = questionnaires[questionNumber].Split('|')[2];
        if (string.Equals(correctAnswer, answer))
        {
            Debug.Log("CORRECT");

            if (removeMe)
            {
                questionnaires.RemoveAt(questionNumber);
            }
        }
        else
        {
            Debug.Log("WRONG");
        }
    }

    public bool TestQuestions()
    {
        if(questionCounter > 5 || questionnaires.Count == 0)
        {
            Finished();
        }

        return questionnaires.Count > 0 ? true : false;
    }

    public void Finished()
    {
        finishRoom.OpenDoor();
        computerUIScript.Close();

        finishRoom.SetFinish(true);
    }

    public string[] GetQuestions()
    {
        return questionnaires.ToArray();
    }

    public bool Delete(int number)
    {
        questionnaires.RemoveAt(number);
        return true;
    }

    //Interfaces....

    //IInteractable

    public void Highlight(bool highlight)
    {
        if (highLightScript)
        {
            highLightScript.Highlight(highlight);
        }
        else
        {
            GetHighLightScript();
            Highlight(highlight);
        }
    }

    public void Interact()
    {
        computerUIScript.gameObject.SetActive(true);
        computerUIScript.Open();
    }

    public void Close()
    {
        PlayerMovementScript.instance.enabled = true;
    }

    //ISaveable...

    public object CaptureState()
    {
        return new SaveData
        {
            questionnaires = this.questionnaires.ToArray(),
        };
    }

    public void LoadState(object state)
    {
        Debug.Log("Computer Loading");

        finishRoom.SetFinish(false);

        SaveData saveData = (SaveData)state;
        questionnaires = new List<string>(saveData.questionnaires);
        questionCounter = 0;
        time = 1000f;
        tries = 1000f;
        computerUIScript.SetOnce();
    }

    [Serializable]
    public struct SaveData
    {
        public string[] questionnaires;
    }

    [ContextMenu("Test")]
    public void Test()
    {
        //Debug.Log(GetQuestion(FuzzyLogic.instance.FuzzyRules(time, tries)));
        //CheckAnswer(questionNumber, "aaa", false);
    }
}