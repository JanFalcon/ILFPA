using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfect : MonoBehaviour
{
    public static PixelPerfect instance;

    public int ppu = 16;

    private void Awake()
    {
        instance = this;
    }

    public Vector3 Position(Vector3 pos)
    {
        return new Vector3(Mathf.Round(pos.x * ppu) / ppu, Mathf.Round(pos.y * ppu) / ppu, pos.z);
    }
}