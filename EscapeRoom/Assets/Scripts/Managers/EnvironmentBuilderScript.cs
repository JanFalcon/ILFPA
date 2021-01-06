using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentBuilderScript : MonoBehaviour, ISaveable
{
    //Interface
    public void LoadFromSaveSystem(SaveSystem saveData)
    {
        
    }

    public void PopulateSaveSystem(SaveSystem saveData)
    {
        saveData.item = null;
    }

    public void SaveJsonData()
    {
        //if(FileManager.)
    }

    public void LoadJsonData()
    {

    }

    void Start()
    {
        //Load from save data.
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
