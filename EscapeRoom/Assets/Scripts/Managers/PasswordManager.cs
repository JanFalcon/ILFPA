using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PasswordManager : MonoBehaviour
{
    public TMP_InputField passwordField;

    public GameObject adminSettingsUI;
    public GameObject adminPanel;

    public string password = "12345";

    public void EnterPassWord()
    {
        if(passwordField.text ==  password)
        {
            adminSettingsUI.SetActive(false);
            adminPanel.SetActive(true);
        }
    }
}
