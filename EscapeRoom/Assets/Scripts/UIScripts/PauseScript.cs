using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    public void Back()
    {
        GameManager.instance.UnPauseGame();
    }

    public void Retry()
    {
        GameManager.instance.Retry();
    }

    public void Menu()
    {
        GameManager.instance.BackToMainMenu();
    }

    public void Quit()
    {
        GameManager.instance.QuitGame();
    }
}
