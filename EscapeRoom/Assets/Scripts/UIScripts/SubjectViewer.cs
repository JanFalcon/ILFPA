using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubjectViewer : MonoBehaviour
{
    private Transform canvas;
    public RectTransform contents;
    public TextMeshProUGUI text;

    public GameObject deleteButton;

    private string path;
    private string subject;

    private void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
    }
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
        ConfirmationScript confirm = ItemCreator.instance.SpawnItem(Item.GameItem.Confimation, canvas).GetComponent<ConfirmationScript>();
        confirm.MethodOverriding = DeleteThis;
        confirm.SetUp($"Are you sure you want to delete this?");
    }

    public bool DeleteThis()
    {
        SaveSystem.instance.DeleteDirectory(path);
        Destroy(gameObject);
        return true;
    }
}
