using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private float timer = 0f;

    private bool CreatorMode = true;

    public GameObject gameCreatorUI, menuPanel;

    public GameObject mainMenuUI, startContentsUI, adminUISettings;

    public GameObject gamePanel, savePanel, endPanel;

    public GameObject gameChooser, subjectViewer, subjectViewerContents, subjectButton;

    public GameObject roomViewer, roomViewerContents, roomButton;

    public GameObject environment;

    private GameObject player;

    private void Awake()
    {
        instance = this;
    }
    
    void Update()
    {
        timer += Time.deltaTime;
    }

    public float GetTimer()
    {
        return timer;
    }

    public bool GetCreatorMode()
    {
        return CreatorMode;
    }

    public void SetCreatorMode(bool CreatorMode)
    {
        this.CreatorMode = CreatorMode;
    }

    public void StartGame()
    {
        mainMenuUI.SetActive(false);
        subjectViewer.SetActive(false);
        startContentsUI.SetActive(true);
        gameChooser.SetActive(true);
    }

    public void Classic()
    {
        gameChooser.SetActive(false);
        subjectViewer.SetActive(true);
        GetSubjects();
    }

    public void Shuffle()
    {
        string[] subjects = SaveManager.instance.GetSubjectFiles();
        string subject = subjects[UnityEngine.Random.Range(0, subjects.Length - 1)].Replace($"{GameManager.instance.GetDesktopPath()}", "");
        string[] rooms = SaveManager.instance.GetSaveFiles(subject);
        SaveSystem.instance.SetPath(rooms[UnityEngine.Random.Range(0, rooms.Length - 1)], "");
        RunGame();
        SaveSystem.instance.Load();
    }

    public void GetSubjects()
    {
        for(int i = 0; i < subjectViewerContents.transform.childCount; i++)
        {
            Destroy(subjectViewerContents.transform.GetChild(i).gameObject);
        }

        foreach (string subject in SaveManager.instance.GetSubjectFiles())
        {
            string _subject = subject.Replace($"{GameManager.instance.GetDesktopPath()}", "");
            Instantiate(subjectButton, subjectViewerContents.transform).GetComponent<SubjectViewer>().SetText(_subject);
        }
    }

    public void GetRooms(string subject)
    {
        subjectViewer.SetActive(false);
        roomViewer.SetActive(true);

        for (int i = 0; i < roomViewerContents.transform.childCount; i++)
        {
            Destroy(roomViewerContents.transform.GetChild(i).gameObject);
        }

        foreach (string room in SaveManager.instance.GetSaveFiles(subject))
        {
            Instantiate(roomButton, roomViewerContents.transform).GetComponent<RoomViewer>().SetText(room);
        }
    }

    public void Admin()
    {
        mainMenuUI.SetActive(false);
        adminUISettings.SetActive(true);
    }

    public void SavePanel()
    {
        PlayerMovementScript.instance.enabled = false;
        gamePanel.SetActive(false);
        savePanel.SetActive(true);
    }

    public void BackToGame()
    {
        PlayerMovementScript.instance.enabled = true;
        savePanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void BackToMenu()
    {
        startContentsUI.SetActive(false);
        adminUISettings.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void EndGame()
    {
        SceneManager.LoadScene(0);
        environment.SetActive(false);
        SetCreatorMode(false);
        if (player)
        {
            Destroy(player);
        }
    }

    public void CreateGame()
    {
        environment.SetActive(true);
        SetCreatorMode(true);
        player = ItemCreator.instance.SpawnItem(Item.GameItem.Player, Vector3.zero);

        menuPanel.SetActive(false);
        gameCreatorUI.SetActive(true);
    }

    public void RunGame()
    {
        SetCreatorMode(false);
        environment.SetActive(true);

        menuPanel.SetActive(false);
    }

    public string GetDesktopPath()
    {
        return $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/SaveData/";
    }

    public void FinishRoom()
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(false);
        savePanel.SetActive(false);
        endPanel.SetActive(true);
    }
}
