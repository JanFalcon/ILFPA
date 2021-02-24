using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game3Buttons : MonoBehaviour
{
    // Start is called before the first frame update
    public int number;
    public Game3UIScript game3UI;

    public void Click()
    {
        AudioPlay();
        game3UI.AddNumber(number);
    }

    public void Check()
    {
        AudioPlay();
        game3UI?.Check();
    }

    public void Clear()
    {
        AudioPlay();
        game3UI?.ClearNumber();
    }

    public void AudioPlay()
    {
        AudioManager.instance.Play("Boop");
    }

}
