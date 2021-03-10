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
    private int difficulty = 1;
    private string difficultTextValue = "Very Easy";
    private Color semiWhite = new Color(1f, 1f, 1f, 0.5f);

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
        difficulty = 1;
        difficultTextValue = "Very Easy";
        HighLightButtons(difficulty, difficultTextValue);
    }

    public void SemiHighLightButtons(int number, string text)
    {
        difficultyText.text = $"Difficulty Rating : {text}";

        for (int i = 0; i < number; i++)
        {
            // if (i < difficulty)
            // {
            //     continue;
            // }
            buttons[i].GetComponent<Image>().color = semiWhite;
        }

        for (int i = number; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Image>().color = Color.black;
        }
    }

    public void HighLightButtons(int number, string text)
    {
        difficulty = number;
        difficultTextValue = text;
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

    public void ExitHighLight()
    {
        HighLightButtons(difficulty, difficultTextValue);
    }

    public void HighLight1()
    {
        SemiHighLightButtons(1, "Very Easy");
    }

    public void HighLight2()
    {
        SemiHighLightButtons(2, "Easy");
    }

    public void HighLight3()
    {
        SemiHighLightButtons(3, "Medium");
    }

    public void HighLight4()
    {
        SemiHighLightButtons(4, "Hard");
    }

    public void HighLight5()
    {
        SemiHighLightButtons(5, "Very Hard");
    }

    public void Click1()
    {
        HighLightButtons(1, "Very Easy");
    }

    public void Click2()
    {
        HighLightButtons(2, "Easy");
    }

    public void Click3()
    {
        HighLightButtons(3, "Medium");
    }

    public void Click4()
    {
        HighLightButtons(4, "Hard");
    }

    public void Click5()
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
