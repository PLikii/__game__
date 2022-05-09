using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character : MonoBehaviour
{
    [SerializeFiled]
    private int lives = 100;

    [SerializeFiled]
    private const float speed = 3.0F;

    [SerializeFiled]
    private const float jumpForce = 15.0F;


    new private Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer sprite;


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if(Impute.GetButton("Horizontal")) Run();
        if(Impute.GetButtonDown("Jump")) Jump();
    }

    private void Run()
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");
        
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }
    
    private void Jump()
    {
        rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }
}

//;'p