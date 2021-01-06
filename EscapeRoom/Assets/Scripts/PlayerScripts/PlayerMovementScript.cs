using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 moveDirection = Vector2.zero;
    public float movementSpeed = 5f;

    private SpriteRenderer spriteRenderer;

    private Animator anim;
    private int isWalking;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        isWalking = Animator.StringToHash("isWalking");

        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveDirection = direction.normalized * movementSpeed;

        Flip(direction.x);

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
}
