using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishRoomScript : MonoBehaviour, IInteractable
{
    public bool finish = false;
    private HighlightScript highlightScript;
    private Transform canvas;
    private bool once = false;
    private void Awake()
    {
        once = false;
        highlightScript = GetComponent<HighlightScript>();
        canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
    }

    public void SetFinish(bool finish)
    {
        this.finish = finish;
    }

    public void OpenDoor()
    {
        if (!once)
        {
            once = true;
            GetComponent<SpriteRenderer>().sortingOrder -= 200;
            GetComponent<HighlightScript>().enabled = false;
            GetComponent<ObjectSortingOrder>().enabled = false;
        }

    }

    //Interface
    // *IInteractable
    public void Highlight(bool highlight)
    {
        highlightScript?.Highlight(highlight);
    }

    public void Interact()
    {
        GameManager.instance.UnInteract();
        PlayerInteract.instance.Close();
        if (!finish)
        {
            AudioManager.instance.Play("Error");
            return;
        }
        ConfirmationScript confirm = ItemCreator.instance.SpawnItem(Item.GameItem.Confimation, canvas).GetComponent<ConfirmationScript>();
        confirm.MethodOverriding = Exit;
        confirm.SetUp("Finish the Game?");
    }

    public bool Exit()
    {
        GameManager.instance.FinishRoom();
        return true;
    }

    public void Close()
    {
    }
}
