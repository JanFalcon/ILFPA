using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ComputerScript : MonoBehaviour, IInteractable, ISaveable
{
    private List<string> questionnaires;
    private List<string> answeredQuestions;

    public GameObject computerPanel;
    public ComputerUIScript computerUIScript;

    private FinishRoomScript finishRoom;
    public Sprite normal, green, red;
    public GameObject light2D;
    private SpriteRenderer spriteRenderer;
    private Coroutine status;
    public static float allocatedTime;
    public float highLightTimer = 10f;
    public float time = 0;
    public float tries = 6;
    private int questionNumber = 0;
    private int questionCounter = 0;

    public float timer = 0f;
    public bool onTimer = false;

    private AudioManager audioManager;
    private HighlightScript highLightScript;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        questionnaires = new List<string>();
        answeredQuestions = new List<string>();
    }

    private void Start()
    {
        audioManager = AudioManager.instance;

        finishRoom = transform.GetChild(0).GetComponent<FinishRoomScript>();

        computerPanel.SetActive(false);
        GetHighLightScript();
    }

    private void Update()
    {
        if (onTimer)
        {
            timer += Time.deltaTime;
        }
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
        return true;
    }

    public void SetValues(float time, float tries)
    {
        this.time = time;
        this.tries = tries;
    }

    public string GetQuestion(int number)
    {
        if (questionnaires.Count > 0)
        {
            questionCounter++;
            return questionnaires[number];
        }
        return "NO QUESTIONS IN THE SYSTEM!";
    }

    public int GetQuestionNumber()
    {
        return CalculateNextQUestion(FuzzyLogic.instance.FuzzyRules(time, tries));
    }

    //TODO: CHANGE CALCULATION!
    public int CalculateNextQUestion(float value)
    {
        float[] fuzzyValues = ListQuestions();
        float difference = 1000f;

        int ctr = 0;
        foreach (float fuzzyValue in fuzzyValues)
        {
            float dif = Math.Abs(value - fuzzyValue);
            if (dif < difference)
            {
                difference = dif;
                questionNumber = ctr;
            }

            ctr++;
        }

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
            audioManager.Play("Correct");
            if (removeMe)
            {
                questionnaires.RemoveAt(questionNumber);
            }
        }
        else
        {
            audioManager.Play("Error");
            Debug.Log("WRONG");
        }
    }

    public void AddAnsweredQuestions(string text)
    {
        answeredQuestions.Add(text);
    }

    public string GetTime(bool stop)
    {
        onTimer = stop;
        return timer.ToString("0.00");
    }

    public string[] GetAnsweredQuestions()
    {
        return answeredQuestions.ToArray();
    }

    public bool TestQuestions()
    {
        if (questionCounter > 20 || questionnaires.Count == 0)
        {
            Finished();
        }

        return questionnaires.Count > 0;
    }

    public void Finished()
    {
        finishRoom.OpenDoor();
        computerUIScript.Close();

        finishRoom.SetFinish(true);
        CheckStatus();
        CheckLaptopsStatus();
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

    public bool Delete(string text)
    {
        questionnaires.Remove(text);
        return true;
    }

    public bool CheckLaptopsStatus()
    {
        bool value = true;
        foreach (LaptopScript laptopScript in FindObjectsOfType<LaptopScript>())
        {
            laptopScript.CheckStatus();
            if (!laptopScript.GetFinish())
            {
                value = false;
            }
        }
        return value;
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
        if (CheckLaptopsStatus() || GameManager.instance.GetCreatorMode())
        {
            audioManager.Play("Boop");
            computerUIScript.gameObject.SetActive(true);
            onTimer = true;
            computerUIScript.Open();
        }
        else
        {
            audioManager.Play("Error");
            CheckStatus();
            Close();
        }
    }

    public void CheckStatus()
    {
        if (status != null)
        {
            StopCoroutine(status);
        }
        status = StartCoroutine(ShowStatus());
    }

    private IEnumerator ShowStatus()
    {
        light2D.SetActive(true);
        spriteRenderer.sprite = finishRoom.finish ? green : red;
        yield return new WaitForSeconds(highLightTimer);
        spriteRenderer.sprite = normal;
        light2D.SetActive(false);
    }

    public void Close()
    {
        PlayerInteract.instance.Close();
    }

    //ISaveable...

    public object CaptureState()
    {
        return new SaveData
        {
            saveAllocatedTime = (allocatedTime * 60f),
            questionnaires = this.questionnaires.ToArray(),
        };
    }

    public void LoadState(object state)
    {
        finishRoom.SetFinish(false);

        SaveData saveData = (SaveData)state;
        GameManager.instance.SetAllocatedTime(saveData.saveAllocatedTime);
        questionnaires = new List<string>(saveData.questionnaires);
        questionCounter = 0;
        time = 0;
        tries = 6;
        computerUIScript.SetOnce();
    }

    [Serializable]
    public struct SaveData
    {
        public float saveAllocatedTime;
        public string[] questionnaires;
    }
}
