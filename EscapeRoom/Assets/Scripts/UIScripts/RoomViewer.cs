using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class RoomViewer : MonoBehaviour
{
    private Transform canvas;
    public RectTransform contents;
    public TextMeshProUGUI text;

    public GameObject deleteButton;

    private string roomName;

    private string fullPath;
    private string subjectPath;

    private void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
    }

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
        ConfirmationScript confirm = ItemCreator.instance.SpawnItem(Item.GameItem.Confimation, canvas).GetComponent<ConfirmationScript>();
        confirm.MethodOverriding = DeleteThis;
        confirm.SetUp($"Are you sure you want to delete this?");
    }

    public bool DeleteThis()
    {
        SaveSystem.instance.DeleteFile(fullPath);
        Destroy(gameObject);
        return true;
    }

}
