using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SaveableEntity : MonoBehaviour, ISaveable
{
    public string id;

    public string GetID => id;

    public Item.GameItem gameItem;

    [ContextMenu("Generate ID")]
    private void GenerateID()
    {
        id = Guid.NewGuid().ToString();
    }

    private void Awake()
    {
        GenerateID();
    }

    private void Start()
    {
        //Subscribe

        if(gameItem != Item.GameItem.Player)
        {
            SaveSystem.instance.LoadEvent += DestroyThis;
        }
    }

    public void DestroyThis()
    {
        //Unsubscribe
        if (gameObject)
        {
            SaveSystem.instance.LoadEvent -= DestroyThis;
            Destroy(gameObject);
        }
    }

    [Serializable]
    private struct SaveData
    {
        public string id, gameItem;
        public float xPos, yPos, zPos;
        public float rotX, rotY, rotZ, rotW;
    }

    public object CaptureState()
    {
        Vector3 objectPosition = transform.position;
        Quaternion ObjectRotation = transform.rotation;

        string itemName = this.gameItem.ToString();

        return new SaveData
        {
            id = this.id,
            gameItem = itemName,
            xPos = objectPosition.x,
            yPos = objectPosition.y,
            zPos = objectPosition.z,

            rotX = ObjectRotation.x,
            rotY = ObjectRotation.y,
            rotZ = ObjectRotation.z,
            rotW = ObjectRotation.w
        };
    }

    public void LoadState(object state)
    {
        SaveData saveData = (SaveData)state;

        transform.position = new Vector3(saveData.xPos, saveData.yPos, saveData.zPos);
        transform.rotation = new Quaternion(saveData.rotX, saveData.rotY, saveData.rotZ, saveData.rotW);
    }

    /// <summary>
    /// It's Process but could be better.
    /// </summary>
    public GameObject LoadObjects(object state)
    {
        SaveData saveData = (SaveData)state;
        Item.GameItem gameItem = (Item.GameItem)System.Enum.Parse(typeof(Item.GameItem), saveData.gameItem);

        if(gameItem == Item.GameItem.Player)
        {
            return gameObject;
        }

        return ItemCreator.instance.SpawnItem(gameItem, Vector3.zero);
    }
}
