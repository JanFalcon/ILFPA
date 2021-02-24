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
        //ItemSlot
        ItemSlot,

        //Interactables
        Book,
        Letter,
        GreenBoard,
        BlackBoard,
        Memo,
        Picture,
        RedPainting,
        GreenPainting,
        BluePainting,

        //Designs
        RedRag,
        GreenRag,
        BlueRag,

        BookShelf,
        BookShelf1,
        BookShelf2,

        Candle,
        Candle1,

        Lamp,
        Lamp1,

        MirrorTable,

        Plant,
        Plant1,
        Plant2,

        Shelf,

        SmallChair,
        SmallShelf,

        Table,

        Window,
        Window1,
        Window2,

        Wall,
        LeftWall,
        RightWall,
        NewGround,

        //Player
        Player,

        //UI
        BookUI,
        BlackBoardUI,
        GreenBoardUI,
        LetterUI,

        Computer,
        Laptop,
        Game1,
        Game2,
        Game3,
        Game4,
        Game5,

        tech1,
        tech2,
        tech3,
        tech4,

        Map1,
        Map2,
        Globe1,
        Globe2,
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
