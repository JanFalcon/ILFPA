using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCreator : MonoBehaviour
{
    public static ItemCreator instance;

    public GameObject player;

    public GameObject itemSlot, confimation;

    public GameObject book, letter;

    public GameObject greenBoard, blackBoard, workTable, studyTable;

    public GameObject memo, picture, redPainting, greenPainting, bluePainting;

    public GameObject redRag, greenRag, blueRag;

    public GameObject bed, bookShelf, bookShelf1, bookShelf2, locker;

    public GameObject candle, candle1;

    public GameObject lamp, lamp1;

    public GameObject mirrorTable, shelf, smallChair, box, box1;

    public GameObject table, table2, table3, table4;

    public GameObject map1, map2, globe1, globe2;

    public GameObject plant, plant1, plant2;

    public GameObject window, window1, window2;

    public GameObject wall, sideWall, sideWallCollider, newGround;

    public GameObject laptop, game1, game2, game3, game4, game5;

    public GameObject tech1;

    private void Awake()
    {
        instance = this;
    }

    ///Function to call to return the data about an item.
    public GameObject GetItem(Item.GameItem item)
    {
        switch (item)
        {
            case Item.GameItem.Player: return player;
            case Item.GameItem.ItemSlot: return itemSlot;
            case Item.GameItem.Confimation: return confimation;

            case Item.GameItem.Laptop: return laptop;
            case Item.GameItem.Game1: return game1;
            case Item.GameItem.Game2: return game2;
            case Item.GameItem.Game3: return game3;
            case Item.GameItem.Game4: return game4;
            case Item.GameItem.Game5: return game5;

            case Item.GameItem.Book: return book;
            case Item.GameItem.Letter: return letter;
            case Item.GameItem.GreenBoard: return greenBoard;
            case Item.GameItem.BlackBoard: return blackBoard;
            case Item.GameItem.Memo: return memo;
            case Item.GameItem.WorkTable: return workTable;
            case Item.GameItem.StudyTable: return studyTable;

            case Item.GameItem.Bed: return bed;
            case Item.GameItem.Picture: return picture;
            case Item.GameItem.RedPainting: return redPainting;
            case Item.GameItem.GreenPainting: return greenPainting;
            case Item.GameItem.BluePainting: return bluePainting;
            case Item.GameItem.RedRag: return redRag;
            case Item.GameItem.GreenRag: return greenRag;
            case Item.GameItem.BlueRag: return blueRag;
            case Item.GameItem.BookShelf: return bookShelf;
            case Item.GameItem.BookShelf1: return bookShelf1;
            case Item.GameItem.BookShelf2: return bookShelf2;
            case Item.GameItem.Locker: return locker;
            case Item.GameItem.Candle: return candle;
            case Item.GameItem.Candle1: return candle1;
            case Item.GameItem.Lamp: return lamp;
            case Item.GameItem.Lamp1: return lamp1;
            case Item.GameItem.MirrorTable: return mirrorTable;
            case Item.GameItem.Map1: return map1;
            case Item.GameItem.Map2: return map2;
            case Item.GameItem.Globe1: return globe1;
            case Item.GameItem.Globe2: return globe2;
            case Item.GameItem.Shelf: return shelf;
            case Item.GameItem.SmallChair: return smallChair;
            case Item.GameItem.box: return box;
            case Item.GameItem.box1: return box1;

            case Item.GameItem.Table: return table;
            case Item.GameItem.Table2: return table2;
            case Item.GameItem.Table3: return table3;
            case Item.GameItem.Table4: return table4;

            case Item.GameItem.Plant1: return plant;
            case Item.GameItem.Plant2: return plant1;
            case Item.GameItem.Plant3: return plant2;
            case Item.GameItem.Curtains: return window;
            case Item.GameItem.Window1: return window1;
            case Item.GameItem.Window2: return window2;
            case Item.GameItem.Wall: return wall;
            case Item.GameItem.SideWall: return sideWall;
            case Item.GameItem.SideWallCollider: return sideWallCollider;
            case Item.GameItem.NewGround: return newGround;

            case Item.GameItem.tech1: return tech1;

            default:
                Debug.Log($"No such thing.. => {item.ToString()}");
                return null;
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
