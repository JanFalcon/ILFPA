using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSortingOrder : MonoBehaviour
{
    private Renderer spriteRenderer;
    public int defaultSortingLayer = 0;
    public float radius = 0.1f;
    public Vector3 offset = Vector3.zero;

    private float YPOSITION = 0;

    public bool update = false;

    private Collider2D[] col;
    private bool once = false;

    private readonly WaitForSeconds updateTime = new WaitForSeconds(0.05f);
    private void Awake()
    {
        col = GetComponents<Collider2D>();
    }

    void Start()
    {
        spriteRenderer = GetComponent<Renderer>();

        //transform.position = PixelPerfect.instance.Position(transform.position);

        //YPOSITION = Mathf.RoundToInt(transform.position.y + offset.y) * -100 + defaultSortingLayer;
        YPOSITION = (int)((transform.position.y + offset.y) * -100) + defaultSortingLayer;
        spriteRenderer.sortingOrder = (int)YPOSITION;
    }

    private void Update()
    {
        if (update)
        {
            UpdateSortingOrder();
        }
    }

    public void UpdateSortingOrder()
    {
        StartCoroutine(UpdateSortingOrderEnum());

        if(col[0] && defaultSortingLayer > 0 && !once)
        {
            foreach(Collider2D col in this.col)
            {
                col.isTrigger = true;
            }
            once = true;
        }
    }

    private IEnumerator UpdateSortingOrderEnum()
    {
        yield return updateTime;
        YPOSITION = (int)((transform.position.y + offset.y) * -100) + defaultSortingLayer;
        //YPOSITION = Mathf.RoundToInt(transform.position.y + offset.y) * -100 + defaultSortingLayer;
        spriteRenderer.sortingOrder = (int)YPOSITION;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position + offset, radius);
    }
}
