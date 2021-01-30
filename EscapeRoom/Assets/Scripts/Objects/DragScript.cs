using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragScript : MonoBehaviour
{
    private Vector2 startPos;
    private ObjectSortingOrder objectSortingOrder;
    private HighlightScript highlightScript;

    private KeyListeners keyListener;
    private SpriteRenderer spriteRenderer;

    private float addSortingLayer = 0;

    public bool dragging = false;
    private void Awake()
    {
        GetKeyListener();
    }

    public void GetKeyListener()
    {
        highlightScript = GetComponent<HighlightScript>();

        keyListener = KeyListeners.instance;
        spriteRenderer = GetComponent<SpriteRenderer>();
        objectSortingOrder = GetComponent<ObjectSortingOrder>();
    }

    private void Update()
    {
        if (dragging)
        {
            Vector3 mousePos = keyListener.mousePos;

            float xPos = mousePos.x - startPos.x;
            float yPos = mousePos.y - startPos.y;
            transform.localPosition = PixelPerfect.instance.Position(new Vector3(xPos, yPos, 0f));

            if (objectSortingOrder)
            {
                addSortingLayer += (Input.GetAxis("Mouse ScrollWheel") * 100);
                objectSortingOrder.defaultSortingLayer = (int)addSortingLayer;
                objectSortingOrder.UpdateSortingOrder();
            }

            if (Input.GetMouseButtonDown(1) && spriteRenderer)
            {
                spriteRenderer.flipX = !spriteRenderer.flipX;
            }

            if (Input.GetKeyDown(KeyCode.V))
            {
                float rotZ = transform.localEulerAngles.z - 90f;
                transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, rotZ);
            }

            if (Input.GetKeyDown(KeyCode.Delete))
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnMouseEnter()
    {
        if (highlightScript)
        {
            highlightScript.Highlight(true);
        }
    }

    private void OnMouseExit()
    {
        if (highlightScript)
        {
            highlightScript.Highlight(false);
        }
    }

    private void OnMouseDown()
    {
        Dragging();
    }

    private void OnMouseUp()
    {
        dragging = false;
    }

    public void Dragging()
    {
        if (!keyListener)
        {
            GetKeyListener();
        }
        Vector3 mousePos = keyListener.mousePos;
        startPos = new Vector2(mousePos.x - transform.localPosition.x, mousePos.y - transform.localPosition.y);
        dragging = true;
    }

    //private void OnMouseDrag()
    //{
    //    Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    //    transform.localPosition = new Vector3(mousePos.x - startPos.x, mousePos.y - startPos.y, 0f);
    //    if (objectSortingOrder)
    //    {
    //        objectSortingOrder.UpdateSortingOrder();
    //    }
    //}
}
