using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void CreateARoom()
    {
        Debug.Log("Create");
        SceneManager.LoadScene(1);
    }

    public void Options()
    {
        Debug.Log("Options");
    }

    public void Quit()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
