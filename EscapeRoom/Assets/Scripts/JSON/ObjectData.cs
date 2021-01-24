using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectData
{
    public string id;
    public Item.GameItem gameItem;

    public Vector3 objectPosition;
    public Quaternion objectRotation;
}
