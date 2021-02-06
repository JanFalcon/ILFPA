using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private float timer = 0f;

    private bool CreatorMode = true;

    public GameObject gameCreatorUI, menuPanel;

    public GameObject mainMenuUI, startContentsUI, adminUISettings;

    public GameObject Environment;

    private void Awake()
    {
        instance = this;
    }
    
    void Update()
    {
        timer += Time.deltaTime;
    }

    public void ResetTime()
    {
        timer = 0;
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
        startContentsUI.SetActive(true);
    }

    public void Admin()
    {
        mainMenuUI.SetActive(false);
        adminUISettings.SetActive(true);
    }

    public void BackToMenu()
    {
        startContentsUI.SetActive(false);
        adminUISettings.SetActive(false);
        mainMenuUI.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void CreateGame()
    {
        Environment.SetActive(true);
        ItemCreator.instance.SpawnItem(Item.GameItem.Player, Vector3.zero);

        menuPanel.SetActive(false);
        gameCreatorUI.SetActive(true);
    }
}
