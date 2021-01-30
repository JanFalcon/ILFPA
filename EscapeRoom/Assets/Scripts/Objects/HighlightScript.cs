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


    public void Highlight(bool highlight)
    {
        if (material)
        {
            material.SetFloat("_Thickness", highlight ? this.highlight : 0f);
        }
    }
}
