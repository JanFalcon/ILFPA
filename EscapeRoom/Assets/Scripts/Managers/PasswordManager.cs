using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PasswordManager : MonoBehaviour
{
    public static PasswordManager instance;

    public TMP_InputField passwordField;

    private Image inputFieldImage;
    private Color inputFieldColor;

    public GameObject adminSettingsUI;
    public GameObject adminPanel;

    public string password = "12345";

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GetProperties();
    }

    public void GetProperties()
    {
        inputFieldImage = passwordField.GetComponent<Image>();
        inputFieldColor = inputFieldImage.color;
    }

    public void EnterPassWord()
    {
        if (passwordField.text == password)
        {
            AdminPermission();
        }
        else
        {
            StartCoroutine(WrongPassword());
        }
    }

    public void AdminPermission()
    {
        GameManager.instance.SetCreatorMode(true);
        adminSettingsUI.SetActive(false);
        adminPanel.SetActive(true);
    }

    public void Reset()
    {
        passwordField.text = "";
        adminPanel.SetActive(false);
        adminSettingsUI.SetActive(true);
    }

    private IEnumerator WrongPassword()
    {
        if (!inputFieldImage)
        {
            GetProperties();
        }
        inputFieldImage.color = Color.red;
        yield return new WaitForSeconds(0.9f);
        inputFieldImage.color = inputFieldColor;
    }
}
