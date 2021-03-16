using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LaptopScript : MonoBehaviour, IInteractable, ISaveable
{
    private List<string> questionnaires;

    public Light2D light2D;
    public GameObject laptopPanel;
    private GameObject laptopUI;
    private Transform canvas;

    private SpriteRenderer spriteRenderer;
    public Sprite normal, green, red;

    public float highLightTimer = 10f;
    public float timer = 0f;
    public bool startTimer = false;
    private int questionnaireCounter = 0;
    private bool finish;

    private Coroutine status;

    LaptopUIScript laptopUIScript;
    private HighlightScript highLightScript;
    private void Awake()
    {
        questionnaires = new List<string>();
        canvas = GameObject.FindGameObjectWithTag("Canvas").transform;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        GetHighLightScript();
    }

    private void GetHighLightScript()
    {
        highLightScript = GetComponent<HighlightScript>();
    }

    private void Update()
    {
        if (startTimer)
        {
            timer += Time.deltaTime;
        }
    }

    public bool Save(string question, string answer)
    {
        if (!string.IsNullOrWhiteSpace(question) && !string.IsNullOrWhiteSpace(answer))
        {
            questionnaires.Add($"{question}|{answer.ToLower()}");
            return true;
        }
        return false;
    }

    public void CheckAnswer()
    {
        //AD UI
        if (TestQuestions())
        {
            laptopUIScript?.SetText(GetQuestionnaire());
        }
        else
        {
            Finish();
        }
        //Add sound??
    }

    public string GetQuestionnaire()
    {
        return questionnaires[questionnaireCounter];
    }

    public void AddQuestionCtr()
    {
        questionnaireCounter++;
    }

    public bool TestQuestions()
    {
        return questionnaireCounter < questionnaires.Count;
    }

    public void Finish()
    {
        finish = true;
        Close();
        CheckStatus();
        //ADD POINTLIGHT
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
        light2D.enabled = true;
        //light2D.lightCookieSprite = sprite;

        spriteRenderer.sprite = TestQuestions() ? red : green;
        yield return new WaitForSeconds(highLightTimer);
        spriteRenderer.sprite = normal;
        light2D.enabled = false;
    }

    public bool Delete(string text)
    {
        questionnaires.Remove(text);
        return true;
    }

    public string[] GetQuestions()
    {
        return questionnaires.ToArray();
    }

    public bool GetFinish()
    {
        return finish;
    }

    //InterFace
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
        if (finish)
        {
            AudioManager.instance.Play("Correct");
            Finish();
            return;
        }

        AudioManager.instance.Play("Boop");

        laptopUI = Instantiate(laptopPanel, canvas);
        laptopUIScript = laptopUI.GetComponent<LaptopUIScript>();
        laptopUIScript.SetLaptopScript(this);

        if (!GameManager.instance.GetCreatorMode())
        {
            CheckAnswer();
        }
    }

    public void Close()
    {
        PlayerInteract.instance.Close();
        Destroy(laptopUI);
    }

    //ISaveable

    public object CaptureState()
    {
        return new SaveData
        {
            questionnaires = this.questionnaires.ToArray()
        };
    }

    public void LoadState(object state)
    {
        finish = false;

        SaveData saveData = (SaveData)state;
        questionnaires = new List<string>(saveData.questionnaires);
    }

    [System.Serializable]
    public struct SaveData
    {
        public string[] questionnaires;
    }
}
