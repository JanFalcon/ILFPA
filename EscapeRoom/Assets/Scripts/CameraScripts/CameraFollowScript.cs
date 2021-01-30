using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public static CameraFollowScript instance;

    private Transform camTransform;
    [SerializeField] private Transform target;

    public float deadZoneRadius;
    [SerializeField] private float fixedRadius;
    public float smoothSpeed = 0f;
    private Vector2 velocity = Vector2.zero;
    private bool chaseTarget = false;

    private void Awake()
    {
        instance = this;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    void Start()
    {
        camTransform = transform.GetChild(0);
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
        if (!target)
        {
            return;
        }

        if (chaseTarget)
        {
            transform.position = Vector2.SmoothDamp((Vector2)transform.position, (Vector2)target.position, ref velocity, smoothSpeed);
        }
    }
}
