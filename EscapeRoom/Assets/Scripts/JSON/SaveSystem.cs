﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem instance;

    private Transform canvas;
    private void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
        if (instance == null)
        {
            instance = this;
        }
    }

    public event Action LoadEvent;

    //SaveData should be subject name

    private string path;
    private string subjectPath;
    public void SetPath(string path, string subjectPath)
    {
        this.path = path;
        this.subjectPath = subjectPath;
    }

    public void Save()
    {
        //Dictionary<string, object> saveData = LoadFile();
        Dictionary<string, object> saveData = new Dictionary<string, object>();
        CaptureState(saveData);
        SaveFile(saveData);

    }

    public void InvokeLoad()
    {
        LoadEvent?.Invoke();
    }

    public void Load()
    {
        InvokeLoad();
        Dictionary<string, object> saveData = LoadFile();
        LoadState(saveData);
    }

    private void SaveFile(object state)
    {
        BinaryFormatter formatter = GetBinaryFormatter();

        if (!Directory.Exists(subjectPath))
        {
            Directory.CreateDirectory(subjectPath);
        }

        FileStream file = File.Create(path);
        formatter.Serialize(file, state);
        file.Close();
    }

    public bool WritePlayerData(string path, string data)
    {
        try
        {
            if (!File.Exists(path))
            {
                FileStream fs = File.Create(path);
                fs.Close();
            }
            StreamWriter writer = new StreamWriter(path);

            writer.WriteLine(data);
            writer.Close();

            return true;
        }
        catch
        {
            ConfirmationScript confim = ItemCreator.instance.SpawnItem(Item.GameItem.Confimation, canvas).GetComponent<ConfirmationScript>();
            confim.MethodOverriding = null;
            confim.SetUp("Error Saving PlayerData");
            return false;
        }
    }



    public Dictionary<string, object> LoadFile()
    {
        if (!File.Exists(path))
        {
            return new Dictionary<string, object>();
        }

        BinaryFormatter formatter = GetBinaryFormatter();
        FileStream file = File.Open(path, FileMode.Open);

        try
        {
            object save = formatter.Deserialize(file);
            file.Close();
            return (Dictionary<string, object>)save;
        }
        catch
        {
            Debug.Log("Failed to load file at " + path);
            file.Close();
            return new Dictionary<string, object>();
        }
    }

    public static BinaryFormatter GetBinaryFormatter()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        return formatter;
    }

    public void CaptureState(Dictionary<string, object> state)
    {
        foreach (SaveableEntity saveEntity in FindObjectsOfType<SaveableEntity>())
        {
            //state.Add(saveEntity.id, saveEntity.CaptureState());
            state[saveEntity.id + "|" + saveEntity.gameItem.ToString()] = saveEntity.CaptureState();
        }
    }

    public void LoadState(Dictionary<string, object> state)
    {
        SaveManager saveManager = SaveManager.instance;
        foreach (KeyValuePair<string, object> item in state)
        {
            if (state.TryGetValue(item.Key, out object loadState))
            {
                string gameItem = item.Key.Split('|')[1];
                if (gameItem.Equals("Computer"))
                {
                    GameObject.FindGameObjectWithTag("Computer").GetComponent<SaveableEntity>().LoadState(loadState);
                }
                else
                {
                    try
                    {
                        GameObject itemValue = saveManager.LoadObject(gameItem);
                        itemValue.GetComponent<SaveableEntity>().LoadState(loadState);
                    }
                    catch
                    {
                        Debug.Log("err");
                        Debug.Log(gameItem);
                    }
                }
            }
        }
    }

    public void DeleteDirectory(string path)
    {
        Directory.Delete(path, true);
    }

    public void DeleteFile(string path)
    {
        File.Delete(path);
    }
}

