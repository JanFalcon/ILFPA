using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomViewer : MonoBehaviour
{
    public RectTransform contents;
    public TextMeshProUGUI text;

    public GameObject deleteButton;

    private string roomName;

    private string fullPath;
    private string subjectPath;

    public void SetText(string text, bool admin)
    {
        deleteButton.SetActive(admin);

        fullPath = text;
        string[] value = fullPath.Split('/');
        roomName = value[value.Length - 1].Replace(".sv", "");
        subjectPath = text.Replace($"{roomName}.sv", "");

        GetComponent<ButtonHighlight>().SetTextBody(roomName);
        this.text.text = roomName;
    }

    public void Clicked()
    {
        GameManager.instance.roomName = roomName;
        SaveSystem.instance.SetPath(fullPath, subjectPath);
        GameManager.instance.RunGame();
        SaveSystem.instance.Load();
    }

    public void Delete()
    {
        SaveSystem.instance.DeleteFile(fullPath);
        Destroy(gameObject);
    }
}
