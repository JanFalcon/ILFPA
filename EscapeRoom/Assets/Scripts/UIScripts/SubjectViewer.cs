using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubjectViewer : MonoBehaviour
{
    public RectTransform contents;
    public TextMeshProUGUI text;

    public GameObject deleteButton;

    private string path;
    private string subject;

    public void SetText(string text, bool admin)
    {
        deleteButton.SetActive(admin);

        path = text;
        subject = text.Replace($"{GameManager.instance.GetDesktopPath()}", "");

        GetComponent<ButtonHighlight>().SetTextBody(subject);
        this.text.text = subject;
    }

    public void Clicked()
    {
        GameManager.instance.subjectName = subject;
        GameManager.instance.GetRooms(subject);
    }

    public void Delete()
    {
        SaveSystem.instance.DeleteDirectory(path);
        Destroy(gameObject);
    }
}
