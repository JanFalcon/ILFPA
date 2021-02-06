using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryScript : MonoBehaviour
{
    private GameObject inventory;

    private void Awake()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory");
    }

    private void Start()
    {
        if (!GameManager.instance.GetCreatorMode())
        {
            enabled = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventory.SetActive(!inventory.activeSelf);
        }
    }
}
