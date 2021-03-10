using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomCreator : MonoBehaviour
{
    public static RoomCreator instance;
    public RectTransform tab1Contents, tab2Contents;

    [HeaderAttribute("Learning Materials")]
    public Item.GameItem[] learningMaterials;

    [HeaderAttribute("Design Materials")]
    public Item.GameItem[] designMaterials;

    private ItemSlotScript itemSlotScript;
    private KeyListeners keyListener;

    private ItemCreator itemCreator;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        keyListener = KeyListeners.instance;
        itemCreator = ItemCreator.instance;

        DisplayInteractables();
    }

    public void DisplayInteractables()
    {
        foreach (Item.GameItem item in learningMaterials)
        {
            itemSlotScript = itemCreator.SpawnItem(Item.GameItem.ItemSlot, tab1Contents).GetComponent<ItemSlotScript>();
            itemSlotScript.SetItem(item);
        }

        foreach (Item.GameItem item in designMaterials)
        {
            itemSlotScript = itemCreator.SpawnItem(Item.GameItem.ItemSlot, tab2Contents).GetComponent<ItemSlotScript>();
            itemSlotScript.SetItem(item);
        }
    }
}
