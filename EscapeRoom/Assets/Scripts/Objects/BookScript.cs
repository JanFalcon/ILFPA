using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BookScript : MonoBehaviour, IInteractable
{
    public GameObject bookUI;
    private GameObject item;
    private string text;

    public float highlight = 0.025f;

    private Material material;

    public void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    private void Start()
    {
        item = ItemCreator.instance.SpawnItem(bookUI, GameObject.FindGameObjectWithTag("Canvas").transform);
        item.SetActive(false);
    }

    public void Highlight(bool highlight)
    {
        material.SetFloat("_Thickness", highlight ? this.highlight : 0f);
    }

    public void Interact()
    {
        if (item)
        {
            item.SetActive(true);
        }
    }

    public void Close()
    {
        //bookUI.SetActive(false);
    }

    public void SetText(string text)
    {
        this.text = text;
    }

    public string GetText()
    {
        return text;
    }
}
