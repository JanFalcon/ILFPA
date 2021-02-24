using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.Universal;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private bool playing = false;
    private bool pause = false;

    private float timer = 0f;
    private float allocatedTime = 0f;

    private bool CreatorMode = false;

    public TextMeshProUGUI roomDesc, timerText;
    public GameObject gameCreatorUI, menuPanel;

    public GameObject mainMenuUI, startContentsUI, adminUISettings;

    public GameObject gamePlayPanel, savePanel, endPanel, pausePanel;

    public GameObject gameChooser, subjectViewer, subjectViewerContents, subjectButton;

    public GameObject roomViewer, roomViewerContents, roomButton;

    public GameObject environment;

    private GameObject player;

    public string subjectName, roomName;

    public Light2D light2D;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        playing = false;
        environment.SetActive(false);
        if (PlayerPrefs.GetInt("Admin") == 1)
        {
            PlayerPrefs.SetInt("Admin", 0);
            Admin();
            PasswordManager.instance.AdminPermission();
        }
    }

    void Update()
    {
        if (playing && !pause)
        {
            if (!GetCreatorMode() && timer >= allocatedTime)
            {
                //END GAME

                // playing = false;
                Debug.Log("TIME PASSED");
                return;
            }

            timer += Time.deltaTime;
            float timeInMin = timer / 60f;
            float timeInSec = timer % 60f;
            float allocTimeInMin = allocatedTime / 60f;
            float allocTimeinSec = allocatedTime % 60f;
            // ?timerText.text = string.Format($"Timer : {(int)timeInMin} {0:0.0} / {1:0.0} M", timer, allocatedTime / 60f);
            timerText.text = $"Timer : {(int)timeInMin}:{(int)timeInSec} / {(int)allocTimeInMin}:{allocTimeinSec}";
        }

        if (Input.GetKeyDown(KeyCode.Escape) && playing)
        {
            if (!pause)
            {
                PauseGame();
            }
            else
            {
                UnPauseGame();
            }
        }
    }

    public void SetAllocatedTime(float allocatedTime)
    {
        this.allocatedTime = allocatedTime;
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

    public void RandomRoom()
    {
        Random.InitState(DateTime.Now.Millisecond);
        string[] subjects = SaveManager.instance.GetSubjectFiles();

        string subject = subjects[Random.Range(0, subjects.Length)].Replace($"{GameManager.instance.GetDesktopPath()}", "");

        string[] rooms = SaveManager.instance.GetSaveFiles(subject);

        string value = rooms[Random.Range(0, rooms.Length)];

        string[] roomDesc = value.Split('/');

        subjectName = roomDesc[roomDesc.Length - 2];
        roomName = roomDesc[roomDesc.Length - 1].Replace(".sv", "");

        SaveSystem.instance.SetPath(value, "");
        RunGame();
        SaveSystem.instance.Load();
    }

    public void GetSubjects()
    {
        for (int i = 0; i < subjectViewerContents.transform.childCount; i++)
        {
            Destroy(subjectViewerContents.transform.GetChild(i).gameObject);
        }

        foreach (string subject in SaveManager.instance.GetSubjectFiles())
        {
            Instantiate(subjectButton, subjectViewerContents.transform).GetComponent<SubjectViewer>().SetText(subject, PlayerPrefs.GetInt("Admin") == 1);
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
            Instantiate(roomButton, roomViewerContents.transform).GetComponent<RoomViewer>().SetText(room, PlayerPrefs.GetInt("Admin") == 1);
        }
    }

    public void Admin()
    {
        mainMenuUI.SetActive(false);
        adminUISettings.SetActive(true);
        PasswordManager.instance.Reset();
    }

    public void Interact()
    {
        gamePlayPanel.SetActive(false);
    }

    public void UnInteract()
    {
        gamePlayPanel.SetActive(true);
    }

    public void SavePanel()
    {
        PlayerMovementScript.instance.enabled = false;
        PlayerInteract.instance.enabled = false;
        gamePlayPanel.SetActive(false);
        savePanel.SetActive(true);
    }

    public void BackToChoosingGameMode()
    {
        if (PlayerPrefs.GetInt("Admin") == 1)
        {
            BackToMenu();
            Admin();
            PasswordManager.instance.AdminPermission();
        }
        else
        {
            subjectViewer.SetActive(false);
            gameChooser.SetActive(true);
        }
    }

    public void BackToSubjectChooser()
    {
        roomViewer.SetActive(false);
        subjectViewer.SetActive(true);
    }

    public void BackToGame()
    {
        PlayerMovementScript.instance.enabled = true;
        PlayerInteract.instance.enabled = true;
        savePanel.SetActive(false);
        gamePlayPanel.SetActive(true);
    }

    public void BackToMenu()
    {
        PlayerPrefs.SetInt("Admin", 0);
        pausePanel.SetActive(false);
        startContentsUI.SetActive(false);
        adminUISettings.SetActive(false);
        mainMenuUI.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void EndGame()
    {
        UnPauseGame();
        environment.SetActive(false);
        SetCreatorMode(false);
        playing = false;
        if (player)
        {
            Destroy(player);
        }
        SceneManager.LoadScene(0);
    }

    public void CreateGame()
    {
        playing = true;
        PlayerPrefs.SetInt("Admin", 1);
        environment.SetActive(true);
        light2D.intensity = 0.5f;

        player = ItemCreator.instance.SpawnItem(Item.GameItem.Player, Vector3.zero);

        menuPanel.SetActive(false);
        gameCreatorUI.SetActive(true);
    }

    public void ViewRooms()
    {
        BackToMenu();
        StartGame();
        PlayerPrefs.SetInt("Admin", 1);
        Classic();
    }

    public void RunGame()
    {
        playing = true;
        bool creator = PlayerPrefs.GetInt("Admin") == 1;
        SetCreatorMode(creator);
        menuPanel.SetActive(false);
        gameCreatorUI.SetActive(creator);
        environment.SetActive(true);

        light2D.intensity = creator ? 0.6f : 0.03f;

        roomDesc.text = $"Subject : {subjectName} | Room : {roomName}";

        if (creator)
        {
            SaveManager.instance.subjectName.text = subjectName;
            SaveManager.instance.saveName.text = roomName;
        }
    }

    public string GetDesktopPath()
    {
        return $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/SaveData/";
    }

    public void PauseGame()
    {
        pause = true;
        Time.timeScale = 0;
        Interact();
        pausePanel.SetActive(true);
    }

    public void UnPauseGame()
    {
        pause = false;
        Time.timeScale = 1;
        UnInteract();
        pausePanel.SetActive(false);
    }

    public void FinishRoom()
    {
        environment.SetActive(false);
        menuPanel.SetActive(false);
        gamePlayPanel.SetActive(false);
        savePanel.SetActive(false);
        endPanel.SetActive(true);

        EndPanelScript.instance.GetValues();

        AudioManager.instance.StartTheme("LittleIdea");

        foreach (SaveableEntity save in FindObjectsOfType<SaveableEntity>())
        {
            Destroy(save.gameObject);
        }
    }
}
