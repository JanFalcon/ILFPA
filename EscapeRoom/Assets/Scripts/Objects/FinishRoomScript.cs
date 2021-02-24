using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishRoomScript : MonoBehaviour, IInteractable
{
    public bool finish = false;
    private HighlightScript highlightScript;

    private void Awake()
    {
        highlightScript = GetComponent<HighlightScript>();
    }

    public void SetFinish(bool finish)
    {
        this.finish = finish;
    }

    public void OpenDoor()
    {
        GetComponent<SpriteRenderer>().sortingOrder -= 100;
    }

    //Interface
    // *IInteractable
    public void Highlight(bool highlight)
    {
        highlightScript?.Highlight(highlight);
    }

    public void Interact()
    {
        if (finish)
        {
            GameManager.instance.FinishRoom();
        }
    }

    public void Close()
    {
    }
}
