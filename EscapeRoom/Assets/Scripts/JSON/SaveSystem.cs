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

    //To be changed
    private string SavePath() => Application.persistentDataPath + "/SaveData/Save.txt";

    [ContextMenu("Save")]
    public void Save()
    {
        //Dictionary<string, object> saveData = LoadFile();
        Dictionary<string, object> saveData = new Dictionary<string, object>();
        CaptureState(saveData);
        SaveFile(saveData);

        Debug.Log("Save Successful");
    }

    [ContextMenu("Load")]
    public void Load()
    {
        LoadEvent?.Invoke();
        Dictionary<string, object> saveData = LoadFile();

        //To be changed
        LoadState(saveData);

        Debug.Log("Load Successful");
    }

    private void SaveFile(object state)
    {
        BinaryFormatter formatter = GetBinaryFormatter();

        if (!Directory.Exists(Application.persistentDataPath + "/SaveData"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/SaveData");
        }

        FileStream file = File.Create(SavePath());
        formatter.Serialize(file, state);
        file.Close();
    }

    public Dictionary<string, object> LoadFile()
    {
        if (!File.Exists(SavePath()))
        {
            return new Dictionary<string, object>();
        }

        BinaryFormatter formatter = GetBinaryFormatter();
        FileStream file = File.Open(SavePath(), FileMode.Open);

        try
        {
            object save = formatter.Deserialize(file);
            file.Close();
            return (Dictionary<string, object>)save;
        }
        catch
        {
            Debug.Log("Failed to load file at " + SavePath());
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
                if (gameItem == "Computer")
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

}

