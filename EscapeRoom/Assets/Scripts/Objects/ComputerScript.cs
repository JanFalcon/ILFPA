using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ComputerScript : MonoBehaviour, IInteractable, ISaveable
{
    private float defuzzification;

    private List<string> questionnaires;

    public GameObject computerPanel;
    public ComputerUIScript computerUIScript;

    public float time = 1000f;
    public float tries = 1000f;

    private int questionNumber = 0;

    private HighlightScript highLightScript;
    private void Awake()
    {
        questionnaires = new List<string>();
    }

    private void Start()
    {
        computerPanel.SetActive(false);
        GetHighLightScript();
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
        return questionnaires.Count > 0 ? true : false;
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
        //computerPanel.SetActive(true);
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

        SaveData saveData = (SaveData)state;
        questionnaires = new List<string>(saveData.questionnaires);
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
