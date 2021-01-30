using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

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
}
