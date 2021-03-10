using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyListeners : MonoBehaviour
{
    public static KeyListeners instance;

    private Camera cam;

    public Vector2 mousePos;

    private DragScript dragScript;

    private bool dragging = false;
    public bool inView = false;
    private void Awake()
    {
        instance = this;
        cam = Camera.main;
    }

    private void Update()
    {
        if (inView)
        {
            return;
        }

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            dragging = true;
            dragScript?.Dragging();
        }

        if (Input.GetMouseButtonDown(1))
        {
        }

        if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
            dragScript?.StopDragging();
        }

        if (Input.GetMouseButtonUp(1))
        {
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

    public void SetDragScript(DragScript dragScript)
    {
        if (this.dragScript != null)
        {
            return;
        }
        this.dragScript = dragScript;
    }

    public void RemoveDragScript()
    {
        if (!dragging)
        {
            dragScript = null;
        }
    }

    public bool GetDragging()
    {
        return dragging;
    }

    public void DestroyObject(GameObject thisObject)
    {
        dragScript = null;
        Destroy(thisObject);
    }
}
