using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EvalViewer : MonoBehaviour
{
    public RectTransform rectTransform;

    public TextMeshProUGUI number;
    public TextMeshProUGUI difficulty;
    public TextMeshProUGUI question;
    public TextMeshProUGUI time;
    public TextMeshProUGUI tries;

    public void SetText(string difficulty, string question, string time, string tries, int number)
    {
        this.number.text = $"{number + 1}";
        this.difficulty.text = difficulty;
        this.question.text = question;
        this.time.text = time;
        this.tries.text = tries;

        this.question.ForceMeshUpdate();
        Vector2 textSize = this.question.GetRenderedValues();

        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, (textSize.y + 20f));

        if (number % 2 == 0)
        {
            GetComponent<Image>().color = new Color(0f, 0f, 0f, 0.15f);
        }
    }
}
