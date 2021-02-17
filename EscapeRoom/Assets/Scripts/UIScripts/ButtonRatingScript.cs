using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonRatingScript : MonoBehaviour
{
    public static ButtonRatingScript instance;

    public TextMeshProUGUI difficultyText;

    public GameObject[] buttons;
    private int difficulty;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ResetValues();
    }

    public void ResetValues()
    {
        HighLightButtons(1, "Very Easy");
    }

    public void HighLightButtons(int number, string text)
    {
        difficulty = number;
        difficultyText.text = $"Difficulty Rating : {text}";

        for (int i = 0; i < number; i++)
        {
            buttons[i].GetComponent<Image>().color = Color.white;
        }

        for (int i = number; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Image>().color = Color.black;
        }
    }

    public void HighLight1()
    {
        HighLightButtons(1, "Very Easy");
    }

    public void HighLight2()
    {
        HighLightButtons(2, "Easy");
    }

    public void HighLight3()
    {
        HighLightButtons(3, "Medium");
    }

    public void HighLight4()
    {
        HighLightButtons(4, "Hard");
    }

    public void HighLight5()
    {
        HighLightButtons(5, "Very Hard");
    }

    public int GetDifficulty()
    {
        return difficulty;
    }

    public Item.Difficulty CheckDifficulty()
    {
        switch (difficulty)
        {
            case 1:
                return Item.Difficulty.VeryEasy;
            case 2:
                return Item.Difficulty.Easy;
            case 3:
                return Item.Difficulty.Medium;
            case 4:
                return Item.Difficulty.Hard;
            case 5:
                return Item.Difficulty.VeryHard;
            default:
                Debug.Log($"NO Such thing... => {difficulty}");
                return Item.Difficulty.VeryEasy;
        }
    }
}
