using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSortingOrder : MonoBehaviour
{
    private Renderer spriteRenderer, itemSpriteRenderer;
    public int defaultSortingLayer = 0;
    public float radius = 0.1f;
    public Vector3 offset = Vector3.zero;

    private float YPOSITION = 0;

    public bool update = false;

    private readonly WaitForSeconds updateTime = new WaitForSeconds(0.05f);
    void Start()
    {
        spriteRenderer = transform.GetChild(0).GetComponent<Renderer>();
        itemSpriteRenderer = transform.GetChild(1).GetComponent<Renderer>();
        //transform.position = PixelPerfect.instance.Position(transform.position);
        //YPOSITION = Mathf.RoundToInt(transform.position.y + offset.y) * -100 + defaultSortingLayer;
        YPOSITION = (int)((transform.position.y + offset.y) * -100) + defaultSortingLayer;
        spriteRenderer.sortingOrder = (int)YPOSITION;
    }

    private void Update()
    {
        if (update)
        {
            StartCoroutine(UpdateSortingOrder());
        }
    }

    private IEnumerator UpdateSortingOrder()
    {
        yield return updateTime;
        YPOSITION = (int)((transform.position.y + offset.y) * -100) + defaultSortingLayer;
        //YPOSITION = Mathf.RoundToInt(transform.position.y + offset.y) * -100 + defaultSortingLayer;
        spriteRenderer.sortingOrder = (int)YPOSITION;
        itemSpriteRenderer.sortingOrder = (int)YPOSITION + 1;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position + offset, radius);
    }
}
