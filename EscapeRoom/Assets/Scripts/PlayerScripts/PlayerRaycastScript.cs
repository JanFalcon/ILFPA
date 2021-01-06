using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycastScript : MonoBehaviour
{

    private Camera cam;
    private RaycastHit hit;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit))
        {
            Debug.Log("MEOW");
        }

        Debug.DrawLine(cam.transform.position, transform.position);
    }

   
}
