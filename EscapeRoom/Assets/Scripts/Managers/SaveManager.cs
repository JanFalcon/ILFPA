﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    public TMP_InputField subjectName;
    public TMP_InputField saveName;
    public TMP_InputField timerField;

    private void Awake()
    {
        instance = this;
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
        Item.GameItem gameItem = (Item.GameItem)System.Enum.Parse(typeof(Item.GameItem), item);
        return ItemCreator.instance.SpawnItem(gameItem, Vector3.zero);
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
        if (string.IsNullOrEmpty(subjectName.text) || string.IsNullOrEmpty(saveName.text) || string.IsNullOrEmpty(timerField.text))
        {
            //!Show Error 

            return;
        }
        SaveSystem.instance.SetPath(GetFullPath(), GetSubjectPath());
        ComputerScript.allocatedTime = float.Parse(timerField.text);
        SaveSystem.instance.Save();

        PlayerPrefs.SetInt("Admin", 1);
        GameManager.instance.EndGame();
    }

    public IEnumerator ErrorWarning()
    {
        yield return null;
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
