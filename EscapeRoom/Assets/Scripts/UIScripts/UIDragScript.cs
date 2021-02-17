using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDragScript : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IPointerEnterHandler
{
    public GameObject linePrefab;
    public string itemName;

    private GameObject line;

    public void OnPointerDown(PointerEventData eventData)
    {
        line = Instantiate(linePrefab, transform.position, Quaternion.identity, transform.root) as GameObject;
        UpdateLine(eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        UpdateLine(eventData.position);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    private void UpdateLine(Vector3 pos)
    {
        //Update Direction
        Vector3 dir = pos - transform.position;
        line.transform.right = dir;

        //Update Scale
        line.transform.localScale = new Vector3(dir.magnitude, 1f, 1f);
    }
}
