using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator;
    public Transform[] firePoints;
    private int facingDirection;
    
    private Shooting shooting;
    
    
    public float restartLevelDelay = 1f;

    
    private Vector2 movement;
    private SpriteRenderer spriteRenderer;
    private bool isLeft;
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    

    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        shooting = gameObject.GetComponent<Shooting>();
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

        if (animator.GetFloat("LastMoveH") == 1)
        {
            facingDirection = 2;
        } else if (animator.GetFloat("LastMoveH") == -1)
        {
            facingDirection = 4;
        } else if (animator.GetFloat("LastMoveY") == 1)
        {
            facingDirection = 1;
        } else if (animator.GetFloat("LastMoveY") == -1)
        {
            facingDirection = 3;
        }
        
        if (Input.GetButtonDown("Fire1") && movement.sqrMagnitude == 0)
        {
            animator.SetTrigger("Attack");
            shooting.Shoot(firePoints[facingDirection - 1]);
        }
        spriteRenderer.flipX = isLeft;
    }

    private void FixedUpdate()
    {
        // Movement
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }
}
