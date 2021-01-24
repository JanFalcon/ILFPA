using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyListeners : MonoBehaviour
{
    public static KeyListeners instance;

    private Camera cam;

    public bool isMouseDown0 = false;
    public bool isMouseDown1 = false;

    public Vector2 mousePos;

    private void Awake()
    {
        instance = this;
        cam = Camera.main;
    }

    private void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            isMouseDown0 = true;
        }

        if (Input.GetMouseButtonDown(1))
        {
            isMouseDown1 = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMouseDown0 = false;
        }

        if (Input.GetMouseButtonUp(1))
        {
            isMouseDown1 = false;
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            PixelPerfect.instance.ppu = 4;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            PixelPerfect.instance.ppu = 8;
        }
    }
}
