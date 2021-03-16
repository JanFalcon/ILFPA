using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    private Transform canvas;
    public static SaveManager instance;

    public TMP_InputField subjectName;
    public TMP_InputField saveName;
    public TMP_InputField timerField;

    private void Awake()
    {
        instance = this;
        canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
    }

    public object CaptureState(string item, Vector3 pos, Quaternion rot)
    {
        return new SaveData
        {
            gameItem = item,
            xPos = pos.x,
            yPos = pos.y,
            zPos = pos.z,
            rotX = rot.x,
            rotY = rot.y,
            rotZ = rot.z,
            rotW = rot.w
        };
    }

    public void LoadState(object state, Transform _transform)
    {
        SaveData saveData = (SaveData)state;

        _transform.position = new Vector3(saveData.xPos, saveData.yPos, saveData.zPos);
        _transform.rotation = new Quaternion(saveData.rotX, saveData.rotY, saveData.rotZ, saveData.rotW);
    }

    public GameObject LoadObject(string item)
    {
        try
        {
            Item.GameItem gameItem = (Item.GameItem)System.Enum.Parse(typeof(Item.GameItem), item);
            return ItemCreator.instance.SpawnItem(gameItem, Vector3.zero);
        }
        catch
        {
            Debug.Log("ERRORER");
            return null;
        }
    }

    [Serializable]
    private struct SaveData
    {
        public string gameItem;
        public float xPos, yPos, zPos;
        public float rotX, rotY, rotZ, rotW;
    }

    public string GetSubjectPath()
    {
        //return $"{Application.persistentDataPath}/SaveData/{subjectName.text.ToLower()}/";
        return $"{GameManager.instance.GetDesktopPath()}{subjectName.text.ToLower()}/";
    }

    public string GetFullPath()
    {
        return $"{GetSubjectPath()}{saveName.text.ToLower()}.sv";
    }

    public void Save()
    {
        if (string.IsNullOrWhiteSpace(subjectName.text) || string.IsNullOrWhiteSpace(saveName.text) || string.IsNullOrWhiteSpace(timerField.text))
        {
            AudioManager.instance.Play("Error");
            //!Show Error 
            if (string.IsNullOrWhiteSpace(subjectName.text))
            {
                StartCoroutine(ErrorWarning(subjectName));
            }

            if (string.IsNullOrWhiteSpace(saveName.text))
            {
                StartCoroutine(ErrorWarning(saveName));
            }

            if (string.IsNullOrWhiteSpace(timerField.text))
            {
                StartCoroutine(ErrorWarning(timerField));
            }

            return;
        }

        AudioManager.instance.Play("Correct");
        GameObject confirm = ItemCreator.instance.SpawnItem(Item.GameItem.Confimation, canvas);
        confirm.GetComponent<ConfirmationScript>().MethodOverriding = SaveThis;
    }

    public bool SaveThis()
    {
        SaveSystem.instance.SetPath(GetFullPath(), GetSubjectPath());
        ComputerScript.allocatedTime = float.Parse(timerField.text);
        SaveSystem.instance.Save();

        PlayerPrefs.SetInt("Admin", 1);
        GameManager.instance.EndGame();
        return true;
    }

    public IEnumerator ErrorWarning(TMP_InputField errorField)
    {
        Image image = errorField.GetComponent<Image>();
        image.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        image.color = Color.white;
    }

    public string[] GetSubjectFiles()
    {
        string path = $"{GameManager.instance.GetDesktopPath()}";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        return Directory.GetDirectories(path);
    }

    public string[] GetSaveFiles(string subject)
    {
        string path = $"{GameManager.instance.GetDesktopPath()}{subject.ToLower()}/";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        return Directory.GetFiles(path);
    }
}
