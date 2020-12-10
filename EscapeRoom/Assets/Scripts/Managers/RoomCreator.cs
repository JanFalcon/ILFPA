using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCreator : MonoBehaviour
{
    public GameObject roomPrefab;
    private GameObject room;

    void Start()
    {
        room = Instantiate(roomPrefab, Vector3.right * 10, Quaternion.identity);
    }

    void Update()
    {
        
    }
}
