using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SaveableEntity))]
public class ObjectData : MonoBehaviour, ISaveable
{
    public object CaptureState()
    {
        string gameItem = GetComponent<SaveableEntity>().gameItem.ToString();
        return SaveManager.instance.CaptureState(gameItem, transform.position, transform.rotation);
    }

    public void LoadState(object state)
    {
        SaveManager.instance.LoadState(state, transform);
    }
}
