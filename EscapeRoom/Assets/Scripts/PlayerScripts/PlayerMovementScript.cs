﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public static PlayerMovementScript instance;

    private Rigidbody2D rb;
    private Vector2 moveDirection = Vector2.zero;
    public float movementSpeed = 5f;

    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;

    private Animator anim;
    private int isWalking;

    private float walkTime = 0f;

    private void Awake()
    {
        instance = this;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        isWalking = Animator.StringToHash("isWalking");

        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        audioSource?.Stop();
    }

    void Start()
    {
        CameraFollowScript.instance.SetTarget(transform);
        AudioManager.instance.AddMixerGroup(audioSource, "PlayerSFX");
    }

    void Update()
    {
        Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveDirection = direction.normalized * movementSpeed;

        if (direction.sqrMagnitude > 0f)
        {
            walkTime += Time.deltaTime;
            if (walkTime > 0.2f)
            {
                audioSource?.UnPause();
            }
        }
        else
        {
            OnDisable();
        }

        //Flip(direction.x);

        anim.SetBool(isWalking, direction != Vector2.zero ? true : false);
    }

    private void Flip(float x)
    {
        spriteRenderer.flipX = x < 0f ? true : x == 0f ? spriteRenderer.flipX : false;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDirection * Time.deltaTime);
    }

    private void OnDisable()
    {
        audioSource?.Stop();
        audioSource?.Play();
        audioSource?.Pause();
        walkTime = 0f;
    }
}
