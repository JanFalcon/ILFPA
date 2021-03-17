using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HighlightScript : MonoBehaviour, ISaveable
{
    private Material material;
    public float highlight = 0.05f;

    private ObjectSortingOrder objectSortingOrder;
    private SpriteRenderer spriteRenderer;
    private float spriteOrder;
    public void Awake()
    {
        objectSortingOrder = GetComponent<ObjectSortingOrder>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        material = GetComponent<SpriteRenderer>().material;
    }

    public void GetSpriteRenderer()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        material?.SetFloat("_Thickness", 0f);
    }

    public void Highlight(bool highlight)
    {
        if (highlight)
        {
            spriteRenderer.sortingOrder = (int)(spriteRenderer.sortingOrder + 10f);
        }
        else
        {
            objectSortingOrder?.UpdateSortingOrder();
        }
        material?.SetFloat("_Thickness", highlight ? this.highlight : 0f);
    }

    //Interface
    //*IISaveable

    public object CaptureState()
    {
        return new SaveData
        {
            flipX = spriteRenderer.flipX,
            sortingOrder = spriteRenderer.sortingOrder,
        };
    }

    public void LoadState(object state)
    {
        SaveData saveData = (SaveData)state;

        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = saveData.flipX;
            spriteRenderer.sortingOrder = saveData.sortingOrder;
        }
        else
        {
            GetSpriteRenderer();
            spriteRenderer.flipX = saveData.flipX;
            spriteRenderer.sortingOrder = saveData.sortingOrder;
        }
    }

    [System.Serializable]
    public struct SaveData
    {
        public bool flipX;
        public int sortingOrder;
    }
}
