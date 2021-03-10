using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public enum GameItem
    {
        ///Write Items here      
        ///Write comments if necessary
        ///Items must be unique

        // !Player
        Player = 0,

        //ItemSlot
        ItemSlot = 1,
        Confimation = 59,
        StudyTable = 64,

        // *Interactables

        Computer = 2,
        Laptop,
        Game1,
        Game2,
        Game3,
        Game4,
        Game5,
        Game6,
        Game7,
        Game8,
        Game9,
        Game10,     //13

        Book,
        OpenBook,
        Letter,
        GreenBoard,
        BlackBoard,
        Memo,       //19
        WorkTable = 60,

        // *Designs

        RedRag = 20,     //20
        GreenRag,
        BlueRag,

        Picture,
        RedPainting,
        GreenPainting,
        BluePainting,

        BookShelf,
        BookShelf1,
        BookShelf2,

        Candle,
        Candle1,

        Lamp,
        Lamp1,

        MirrorTable,

        Plant1,
        Plant2,
        Plant3,

        Shelf,

        SmallChair,
        box,
        box1,

        Table,
        Table2,
        Table3 = 61,
        Table4,

        Curtains = 44,
        Window1,
        Window2,

        Wall,
        SideWall,
        NewGround,

        tech1,
        tech2,
        tech3,
        tech4,

        Map1,
        Map2,
        Globe1,
        Globe2,
        Bed,        //58

        Locker = 63,
        SideWallCollider = 65,
    }
    public GameItem gameItem;


    public enum Difficulty
    {
        VeryEasy,
        Easy,
        Medium,
        Hard,
        VeryHard,
    }
    public Difficulty difficulty;
}
