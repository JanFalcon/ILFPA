using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Game4Button : MonoBehaviour
{
    private Game4UIScript game4UI;
    private int value;

    private Image myImage;
    public Image image;

    private void Awake()
    {
        GetImage();
    }

    public void GetImage()
    {
        myImage = GetComponent<Image>();
    }

    public void SetValue(int value, Game4UIScript game4UI)
    {
        this.game4UI = game4UI;
        this.value = value;

        switch (value)
        {
            case 1:
                image.color = Color.red;
                break;
            case 2:
                image.color = Color.yellow;
                break;
            case 3:
                image.color = Color.green;
                break;
            case 4:
                image.color = Color.blue;
                break;
            case 5:
                image.color = Color.cyan;
                break;
            case 6:
                image.color = Color.magenta;
                break;
        }
    }


    public int GetValue()
    {
        return value;
    }

    public void HighLight()
    {
        if (myImage)
        {
            myImage.color = Color.yellow;
        }
        else
        {
            GetImage();
        }
    }

    public void RemoveHighLight()
    {
        myImage.color = Color.white;
    }

    public void CorrectAnswer()
    {
        myImage.color = Color.green;
    }

    public void WrongAnswer()
    {
        myImage.color = Color.red;
    }

    public void Clicked()
    {
        game4UI?.Compare(this);
    }

    public void DestroyThis()
    {
        Destroy(gameObject);
    }
}
