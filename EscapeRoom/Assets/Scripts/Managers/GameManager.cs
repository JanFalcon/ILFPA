using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private float timer = 0f;

    private bool CreatorMode = true;

    private void Awake()
    {
        instance = this;
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;
    }

    public float GetTimer()
    {
        return timer;
    }

    public bool GetCreatorMode()
    {
        return CreatorMode;
    }

    public void SetCreatorMode(bool CreatorMode)
    {
        this.CreatorMode = CreatorMode;
    }

}
