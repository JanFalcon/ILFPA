using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BookScript : MonoBehaviour, IInteractable
{
    public GameObject bookUI;

    private Material material;

    [TextArea(3, 10)]
    public string[] text;

    public void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    public void Highlight(bool highlight)
    {
        material.SetFloat("_Thickness", highlight ? 0.025f : 0f);
    }

    public void Interact()
    {
        bookUI.SetActive(true);
    }

    public void Close()
    {
        bookUI.SetActive(false);
    }
}
