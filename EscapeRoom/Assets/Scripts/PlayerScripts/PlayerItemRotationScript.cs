﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;

public class PlayerItemRotationScript : MonoBehaviour
{
    public static PlayerItemRotationScript instance;

    public Transform flashLight;
    public Light2D light2D;
    public float objectRotationSpeed = 5f;

    private Camera cam;
    private Quaternion objectRotation;
    private SpriteRenderer playerSprite;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        light2D.intensity = GameManager.instance.GetCreatorMode() ? 0.15f : 0.75f;

        cam = Camera.main;
        playerSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Vector3 difference = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        objectRotation = Quaternion.Euler(0f, 0f, (rotZ - 90f));
        RotateObject();

        float worldXPosition = cam.ScreenToWorldPoint(Input.mousePosition).x;
        playerSprite.flipX = worldXPosition < transform.position.x;
    }

    private void RotateObject()
    {
        flashLight.localRotation = Quaternion.Slerp(flashLight.localRotation, objectRotation, objectRotationSpeed * Time.deltaTime);
    }
}
