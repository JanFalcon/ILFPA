using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem instance;

    private void Awake()
    {
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

    public void Load()
    {
        LoadEvent?.Invoke();
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
                    GameObject itemValue = saveManager.LoadObject(gameItem);
                    itemValue.GetComponent<SaveableEntity>().LoadState(loadState);
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

