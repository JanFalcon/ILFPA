using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestionViewer : MonoBehaviour
{
    private Transform canvas;
    public RectTransform rectTransform;
    public TextMeshProUGUI text;
    private ComputerUIScript computerUI;
    private int number;
    private void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
    }
    public void SetText(string text, int number, ComputerUIScript computerUI)
    {
        text = text.Replace($"{(char)13}", "");

        this.computerUI = computerUI;
        gameObject.SetActive(true);
        this.number = number;
        this.text.text = text;
        this.text.ForceMeshUpdate();
        Vector2 textSize = this.text.GetRenderedValues();

        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, textSize.y + 0.5f);

        if (number % 2 == 0)
        {
            Color color = GetComponent<Image>().color;
            GetComponent<Image>().color = new Color(color.r, color.g, color.b, 0.5f); ;
        }
    }

    public void Delete()
    {
        AudioManager.instance.Play("Boop");
        ConfirmationScript confirm = ItemCreator.instance.SpawnItem(Item.GameItem.Confimation, canvas).GetComponent<ConfirmationScript>();
        confirm.MethodOverriding = DeleteThis;
        confirm.SetUp($"Are you sure you want to delete this?");
    }

    public bool DeleteThis()
    {
        if (computerUI.DeleteThis(number))
        {
            Destroy(gameObject);
        }
        computerUI.SetQuestionViewer();
        return true;
    }

}
