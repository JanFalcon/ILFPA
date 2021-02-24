using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryScript : MonoBehaviour
{
    private GameObject inventory;

    private void Awake()
    {
        FindInventory();
    }

    public void FindInventory()
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!inventory)
            {
                FindInventory();
            }
            if (inventory)
            {
                inventory.SetActive(!inventory.activeSelf);
            }
        }
    }
}
