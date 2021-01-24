using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabScript : MonoBehaviour
{
    public bool activeOnStart = false;
    public GameObject otherTab;
    private GameObject tabContents;

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
        tabContents.SetActive(true);
    }
}
