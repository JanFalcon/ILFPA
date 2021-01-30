using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//For Position
public class ObjectData : MonoBehaviour, ISaveable
{
    public Item.GameItem gameItem;

    public object CaptureState()
    {
        return SaveManager.instance.CaptureState(gameItem.ToString(), transform.position, transform.rotation);
    }

    public void LoadState(object state)
    {
        SaveManager.instance.LoadState(state, transform);
    }
}
