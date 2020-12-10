using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    private Transform camTransform;
    [SerializeField] private Transform target;

    public float deadZoneRadius;
    [SerializeField] private float fixedRadius;
    public float smoothSpeed = 0f;
    private Vector2 velocity = Vector2.zero;
    private bool chaseTarget = false;

    void Start()
    {
        camTransform = transform.GetChild(0);
        target = GameObject.FindGameObjectWithTag("Player").transform;

        fixedRadius = (1f / 15f) * deadZoneRadius;
    }

    void Update()
    {
        if (!target)
        {
            return;
        }

        if (Vector2.Distance(target.position, transform.position) >= deadZoneRadius)
        {
            chaseTarget = true;
        }
        else if (Vector2.Distance(target.position, transform.position) < fixedRadius && chaseTarget)
        {
            chaseTarget = false;
        }
    }

    private void LateUpdate()
    {
        if (chaseTarget)
        {
            transform.position = Vector2.SmoothDamp((Vector2)transform.position, (Vector2)target.position, ref velocity, smoothSpeed);
        }
    }
}
