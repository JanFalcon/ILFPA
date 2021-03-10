using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

public class Game1UIScript : MonoBehaviour
{
    public Transform contentsBG;
    public GameObject circleThing;
    private int correct = 0;
    public GameObject message;
    public GameObject buttons;

    public TMP_InputField inputField;

    private Game1Script game1Script;
    private void Awake()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
    }

    public void Check()
    {
        correct++;
        if (correct == 9)
        {
            Swap();
        }
    }

    public void Open(Game1Script game1Script, string hiddenMessage)
    {
        this.game1Script = game1Script;
        inputField.text = hiddenMessage;
        PopulateArea();
        if (!GameManager.instance.GetCreatorMode())
        {
            inputField.interactable = false;
            message.SetActive(false);
            buttons.SetActive(false);
        }
        else
        {
            contentsBG.gameObject.SetActive(false);
        }

    }

    public void PopulateArea()
    {
        for (int i = 0; i < contentsBG.childCount; i++)
        {
            Destroy(contentsBG.GetChild(i));
        }

        for (int i = 0; i < 9; i++)
        {
            float xPos = Random.Range(-470f, -100f);
            float yPos = Random.Range(-275f, 275f);
            Instantiate(circleThing, contentsBG).GetComponent<UIDragScript>().SetValues(new Vector2(xPos, yPos), i.ToString(), GetColor(i), this);
        }

        for (int i = 0; i < 9; i++)
        {
            float xPos = Random.Range(100f, 470f);
            float yPos = Random.Range(-275f, 275f);
            Instantiate(circleThing, contentsBG).GetComponent<UIDragScript>().SetValues(new Vector2(xPos, yPos), i.ToString(), GetColor(i), this);
        }
        correct = 0;
    }

    public Color GetColor(int number)
    {
        switch (number)
        {
            case 1: return Color.red;
            case 2: return Color.black;
            case 3: return Color.blue;
            case 4: return Color.cyan;
            case 5: return Color.green;
            case 6: return Color.magenta;
            case 7: return Color.yellow;
            case 8: return Color.gray;
            default: return Color.white;
        }
    }

    public void Save()
    {
        if (game1Script.Save(inputField.text))
        {
            Close();
        }
    }

    public void Swap()
    {
        contentsBG.gameObject.SetActive(!contentsBG.gameObject.activeSelf);
        message.SetActive(!message.activeSelf);
    }

    public void Clear()
    {
        inputField.text = "";
    }

    public void Close()
    {
        AudioManager.instance.Play("Boop");
        GameManager.instance.UnInteract();
        game1Script?.Close();
    }
}
