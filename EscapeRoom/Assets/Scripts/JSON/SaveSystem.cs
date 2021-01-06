using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveSystem
{
    public GameObject item;

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public void LoadFromJson(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);
    }
}

public interface ISaveable
{
    void PopulateSaveSystem(SaveSystem saveData);
    void LoadFromSaveSystem(SaveSystem saveData);
}
