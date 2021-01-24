using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public static SaveData _instance;
    public static SaveData Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new SaveData();
            }
            return _instance;
        }
    }

    public List<ObjectData> objectData;

}
