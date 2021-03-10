using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SaveableEntity : MonoBehaviour
{
    public string id;
    public Item.GameItem gameItem;

    public bool createItem = true;

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
        if (createItem)
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

    public object CaptureState()
    {
        Dictionary<string, object> state = new Dictionary<string, object>();

        foreach (ISaveable saveAble in GetComponents<ISaveable>())
        {
            state[saveAble.GetType().ToString()] = saveAble.CaptureState();
        }

        return state;
    }

    public void LoadState(object state)
    {
        Dictionary<string, object> stateDictionary = (Dictionary<string, object>)state;
        foreach (ISaveable saveAble in GetComponents<ISaveable>())
        {
            string typeName = saveAble.GetType().ToString();
            if (stateDictionary.TryGetValue(typeName, out object value))
            {
                saveAble.LoadState(value);
            }
        }
    }

}
