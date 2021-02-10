using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HighlightScript : MonoBehaviour
{
    private Material material;
    public float highlight = 0.025f;


    public void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    private void Start()
    {
        material?.SetFloat("_Thickness", 0f);
    }

    public void Highlight(bool highlight)
    {
        material?.SetFloat("_Thickness", highlight ? this.highlight : 0f);
    }
}
