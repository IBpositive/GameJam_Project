using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator;
    
    public float restartLevelDelay = 1f;

    
    private Vector2 movement;
    private SpriteRenderer spriteRenderer;
    private bool isLeft;
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");

    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        isLeft = false;
    }

    void Update()
    {
        // Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        
        
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (Input.GetAxisRaw("Horizontal") == 1 || 
            Input.GetAxisRaw("Horizontal") == -1 || 
            Input.GetAxisRaw("Vertical") == 1 || 
            Input.GetAxisRaw("Vertical") == -1)
        {
            animator.SetFloat("LastMoveH", Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("LastMoveY",Input.GetAxisRaw("Vertical"));
            // inverse sprite if last position was facing left.
            if (Input.GetAxisRaw("Horizontal") == -1)
            {
                isLeft = true;
            }
            else
            {
                isLeft = false;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && movement.sqrMagnitude == 0)
        {
            animator.SetTrigger("Attack");
        }
        spriteRenderer.flipX = isLeft;
    }

    private void FixedUpdate()
    {
        // Movement
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }
}
