using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubjectViewer : MonoBehaviour
{
    public RectTransform contents;
    public TextMeshProUGUI text;

    private string subject;

    public void SetText(string text)
    {
        subject = text;

        GetComponent<ButtonHighlight>().SetTextBody(subject);
        this.text.text = text;
    }

    public void Clicked()
    {
        GameManager.instance.GetRooms(subject);
    }
}
