using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotScript : MonoBehaviour
{
    public Image image;
    private Item.GameItem item;

    public void SetItem(Item.GameItem item)
    {
        this.item = item;
        image.sprite = ItemCreator.instance.GetSprite(this.item);

        image.SetNativeSize();
        transform.GetChild(0).GetComponent<RectTransform>().localScale = new Vector3(2f, 2f, 1f);
    }

    public void Clicked()
    {
        try
        {
            ItemCreator.instance.SpawnItem(item, KeyListeners.instance.mousePos).GetComponent<DragScript>().Dragging();
        }
        catch(Exception ee)
        {
            Debug.Log("No Item Found");
        }
    }

}
