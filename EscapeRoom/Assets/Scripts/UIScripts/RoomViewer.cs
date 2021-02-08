using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomViewer : MonoBehaviour
{
    public RectTransform contents;
    public TextMeshProUGUI text;

    private string roomName;

    private string fullPath;
    private string subjectPath;

    public void SetText(string text)
    {
        string[] value = text.Split('/');
        roomName = value[value.Length - 1].Replace(".sv", "");
        fullPath = text;
        subjectPath = text.Replace($"{roomName}.sv", "");

        GetComponent<ButtonHighlight>().SetTextBody(roomName);
        this.text.text = roomName;
    }

    public void Clicked()
    {
        SaveSystem.instance.SetPath(fullPath, subjectPath);
        GameManager.instance.RunGame();
        SaveSystem.instance.Load();
    }
}
