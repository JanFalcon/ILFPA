using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestionViewer2 : MonoBehaviour
{
    private Transform canvas;
    public RectTransform rectTransform;
    public TextMeshProUGUI text;

    private LaptopUIScript laptopUI;

    private void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
    }
    public void SetText(string text, int number, LaptopUIScript laptopUI)
    {
        text = text.Replace($"{(char)13}", "");

        this.laptopUI = laptopUI;
        gameObject.SetActive(true);

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
        if (laptopUI.Delete(text.text))
        {
            Destroy(gameObject);
        }
        return true;
    }
}
