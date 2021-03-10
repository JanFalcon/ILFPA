using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabScript : MonoBehaviour
{
    public bool activeOnStart = false;
    public GameObject otherTab;
    private GameObject tabContents;

    public ScrollRect scrollRect;
    public RectTransform myContents, myViewPort;

    void Awake()
    {
        tabContents = transform.GetChild(1).gameObject;
        tabContents.SetActive(activeOnStart);
    }

    public void Clicked()
    {
        if (otherTab)
        {
            otherTab.SetActive(false);
        }
        scrollRect.content = myContents;
        scrollRect.viewport = myViewPort;
        tabContents.SetActive(true);
    }
}
