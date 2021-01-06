using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaveSystemScript : MonoBehaviour, ISaveable
{
    public void LoadFromSaveSystem(SaveSystem saveData)
    {

    }

    public void PopulateSaveSystem(SaveSystem saveData)
    {
        saveData.item = this.gameObject;
    }

    public void SaveData()
    {
        SaveSystem ss = new SaveSystem();
        PopulateSaveSystem(ss);

        //if(File.)
    }

    void Start()
    {
        
    }

}
