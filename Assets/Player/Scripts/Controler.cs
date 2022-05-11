using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    //in order to control both players using 1 script.
    public int playerIdx;


    //Variables.
    public float shiftSpeed;
    public float movementSpeed;
    public float jumpForce;
    private float speed;

    //Ground stuff.
    public LayerMask whatIsGround;
    public bool grounded;

    //boolean stuff.
    private bool facingRight;
    private bool moving;

    //Needed to check if player is on the ground.
    public Transform groundCheck;

    //Limit player's movement speed.
    public float maxMovementSpeed = 400f;

    //Double jump stuff.
    private bool doubleJumpReady = false;

    //rb
    private Rigidbody2D rb;

    Animator animator;


    bool run;
    //bool jump;

    // Start is called before the first frame update
    void Start()
    {
        doubleJumpReady = true;
        rb = GetComponent<Rigidbody2D>();
        facingRight = true;
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        run = false;
        
        //if(jump == true || grounded == false)
            //jump = true;
        //else
            //jump = false;

        

        if (Input.GetKey(KeyCode.LeftShift))
                speed = shiftSpeed;

        if(!Input.GetKey(KeyCode.LeftShift))
                speed = movementSpeed;
        
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            Move(-1);

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            Move(1);

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
            Jump();

        if (run == false)
        {
            animator.SetBool("Run", false);
            //animator.SetBool("Jump", false);
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        SlowDown();
    }

    private void LateUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, whatIsGround);

        if (grounded)
            doubleJumpReady = true;

    }

    private void SlowDown()
    {
        Debug.Log("SlowDown");
        if (moving) return;

        //if player is not moving, slow them down.
        if (rb.velocity.x > 0.2f)
            rb.AddForce(speed * Time.deltaTime * -Vector2.right);
        if (rb.velocity.x < -0.2f)
            rb.AddForce(speed * Time.deltaTime * Vector2.right);
    }

    public void Move(int dir)
    {
        Debug.Log("Move");
        run = true;
        animator.SetBool("Run", true);
        //animator.SetBool("Jump", false);

        //Flip the player.
        Flips(dir);

        //Moving the player.
        moving = true;

        float xVel = rb.velocity.x;//Get x velocity.

        if (dir > 0)
            rb.AddForce(speed * Time.deltaTime * Vector2.right * dir);
        else if (dir < 0)
            rb.AddForce(speed * Time.deltaTime * Vector2.right * dir);

        //Help player turn around faster.
        if (xVel > 0.2f && dir < 0)
            rb.AddForce(speed * 3.2f * Time.deltaTime * -Vector2.right);
        if (xVel < 0.2f && dir > 0)
            rb.AddForce(speed * 3.2f * Time.deltaTime * Vector2.right);
    }

    private void Flips(int dir)
    {
        Debug.Log("Flips");

        if (facingRight && dir == -1 || !facingRight && dir == 1)
        {
            facingRight = !facingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }

    protected void Jump()
    {
        Debug.Log("Jump");
        run = true;
        animator.SetBool("Run", false);
        //animator.SetBool("Jump", true);


        if (grounded)
        {
            rb.AddForce(Vector2.up * jumpForce);
            doubleJumpReady = true;
        }
        else if (!grounded && doubleJumpReady)
        {
            rb.AddForce(Vector2.up * jumpForce);
            doubleJumpReady = false;
            //Debug.Log("I am double jumping");
        }
    }
}

