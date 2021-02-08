using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishRoomScript : MonoBehaviour
{
    public bool finish = false;

    public void SetFinish(bool finish)
    {
        this.finish = finish;
    }

    public void OpenDoor()
    {
        GetComponent<SpriteRenderer>().sortingOrder -= 100;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(finish && collision.CompareTag("Player"))
        {
            Debug.Log("FINISH");
        }
    }
}
