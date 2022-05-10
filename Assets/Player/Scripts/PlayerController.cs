using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

     public float moveSpeed;
    public float jumpForce;
    public float wallSlidingSpeed;
    public float pushOutFromWallForce;
    public float wallJumpTime;
    public int jumpsAmount;
    public int jumpsLeft;

    public Transform GroundCheck;
    public LayerMask GroundLayer;

    float moveInput;
    public bool isGrounded;

    Rigidbody2D rb2d;
    float scaleX;

    bool touchingWall;
    bool wallJumping;
    public Transform WallCheck;
    public LayerMask WallLayer;
    public float speed;
    public float jumpheight;
    Animator anim;

    void Start()    
    {
        rb2d = GetComponent<Rigidbody2D>();
        scaleX = transform.localScale.x;
    }
    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        
        Jump();
        WallJump();
    }
    void Flip()
    {
        if (Input.GetAxis("Horizontal") > 0)
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        if (Input.GetAxis("Horizontal") < 0)
        transform.localRotation = Quaternion.Euler(0, 180, 0);
    }

    private void FixedUpdate()
    {
        if (!wallJumping)
        {
            Move();
        }
        CheckIfTouchingWall();
        WallSlide();
    }

    public void Move()
    {
        Flip();
        rb2d.velocity = new Vector2(moveInput * moveSpeed, rb2d.velocity.y);
    }

    public void Flip()
    {
        if (moveInput > 0)
        {
            transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);            
        }
        if (moveInput < 0)
        {
            transform.localScale = new Vector3((-1)*scaleX, transform.localScale.y, transform.localScale.z);
        }

    }

    public void Jump()
    {              
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckIfGrounded();

            if (jumpsLeft > 0)
            {
                jumpsLeft--;
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
            }
                        
        }        
    }

    public void CheckIfGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, GroundCheck.GetComponent<CircleCollider2D>().radius, GroundLayer);
        ResetJumps();
    }

    public void ResetJumps()
    {
        if (isGrounded)
        {
            jumpsLeft = jumpsAmount;
        }
    }

    public void CheckIfTouchingWall()
    {
        touchingWall = Physics2D.OverlapCircle(WallCheck.position, WallCheck.GetComponent<CircleCollider2D>().radius, WallLayer);
    }

    public void WallSlide()
    {
        if (touchingWall)
        {
            if (moveInput == 0)
            {
                touchingWall = false;
            }
            else
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, Mathf.Clamp(rb2d.velocity.y, -wallSlidingSpeed, float.MaxValue));
            }
            
        }
    }

    public void NotWallJumping()
    {
        wallJumping = false;
    }

    public void WallJump()
    {
        if(touchingWall && !isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                wallJumping = true;
                rb2d.velocity = new Vector2(-moveInput * pushOutFromWallForce, jumpForce);
                Invoke("NotWallJumping", wallJumpTime);
            }
        }
    }
}