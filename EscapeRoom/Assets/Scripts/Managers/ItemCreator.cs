using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCreator : MonoBehaviour
{
    public static ItemCreator instance;

    public GameObject player;

    public GameObject itemSlot;
    public GameObject bookUI, blackBoardUI, greenBoardUI;

    public GameObject book, letter;

    public GameObject greenBoard, blackBoard;

    public GameObject memo, picture, redPainting, greenPainting, bluePainting;

    public GameObject redRag, greenRag, blueRag;

    public GameObject bookShelf, bookShelf1, bookShelf2;

    public GameObject candle, candle1;

    public GameObject lamp, lamp1;

    public GameObject mirrorTable, shelf, smallChair, smallShelf, table;

    public GameObject plant, plant1, plant2;

    public GameObject window, window1, window2;

    public GameObject wall, leftWall, rightWall, newGround;

    private void Awake()
    {
        instance = this;
    }

    ///Function to call to return the data about an item.
    public GameObject GetItem(Item.GameItem item)
    {
        switch (item)
        {
            case Item.GameItem.ItemSlot:        return itemSlot;
            case Item.GameItem.Book:            return book;
            case Item.GameItem.Letter:          return letter;
            case Item.GameItem.GreenBoard:      return greenBoard;
            case Item.GameItem.BlackBoard:      return blackBoard;
            case Item.GameItem.Memo:            return memo;
            case Item.GameItem.Picture:         return picture;
            case Item.GameItem.RedPainting:     return redPainting;
            case Item.GameItem.GreenPainting:   return greenPainting;
            case Item.GameItem.BluePainting:    return bluePainting;
            case Item.GameItem.RedRag:          return redRag;
            case Item.GameItem.GreenRag:        return greenRag;
            case Item.GameItem.BlueRag:         return blueRag;
            case Item.GameItem.BookShelf:       return bookShelf;
            case Item.GameItem.BookShelf1:      return bookShelf1;
            case Item.GameItem.BookShelf2:      return bookShelf2;
            case Item.GameItem.Candle:          return candle;
            case Item.GameItem.Candle1:         return candle1;
            case Item.GameItem.Lamp:            return lamp;
            case Item.GameItem.Lamp1:           return lamp1;
            case Item.GameItem.MirrorTable:     return mirrorTable;
            case Item.GameItem.Shelf:           return shelf;
            case Item.GameItem.SmallChair:      return smallChair;
            case Item.GameItem.SmallShelf:      return smallShelf;
            case Item.GameItem.Table:           return table;
            case Item.GameItem.Plant:           return plant;
            case Item.GameItem.Plant1:          return plant1;
            case Item.GameItem.Plant2:          return plant2;
            case Item.GameItem.Window:          return window;
            case Item.GameItem.Window1:         return window1;
            case Item.GameItem.Window2:         return window2;
            case Item.GameItem.Wall:            return wall;
            case Item.GameItem.LeftWall:        return leftWall;
            case Item.GameItem.RightWall:       return rightWall;
            case Item.GameItem.NewGround:       return newGround;

            case Item.GameItem.Player:          return player;

            case Item.GameItem.BookUI:          return bookUI;
            case Item.GameItem.BlackBoardUI:    return blackBoardUI;
            case Item.GameItem.GreenBoardUI:    return greenBoardUI;
            default:
                Debug.Log("No such thing..");   return null;
        }
    }

    ///Function to get item sprite.
    public Sprite GetSprite(Item.GameItem item)
    {
        return GetItem(item).GetComponent<SpriteRenderer>().sprite;
    }

    ///Function to call to spawn item.
    public GameObject SpawnItem(Item.GameItem item, Transform parent)
    {
        return SpawnGameObject(GetItem(item), parent);
    }

    private GameObject SpawnGameObject(GameObject _gameObject, Transform parent)
    {
        return Instantiate(_gameObject, parent.position, parent.rotation, parent) as GameObject;
    }

    ///Method Overloading
    public GameObject SpawnItem(Item.GameItem item, Vector2 pos)
    {
        return SpawnGameObject(GetItem(item), pos);
    }

    private GameObject SpawnGameObject(GameObject _gameObject, Vector2 pos)
    {
        return Instantiate(_gameObject, pos, Quaternion.identity) as GameObject;
    }

    ///Funtion to call to spawn GameObject
    public GameObject SpawnItem(GameObject item, Transform parent)
    {
        return SpawnGameObject(item, parent);
    }

    public GameObject SpawnItem(GameObject item, Vector2 pos)
    {
        return SpawnGameObject(item, pos);
    }
}
