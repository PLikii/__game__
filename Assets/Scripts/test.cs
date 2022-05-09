using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character : MonoBehaviour
{
    [SerializeFiled]
    private int lives = 100;

    [SerializeFiled]
    private float speed = 3.0F;

    [SerializeFiled]
    private float jupmForce = 15.0F;


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
        Vector3 direction = transform.right * Imput.GetAxeis("Horizontal");
        
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }
    
    private void Jump()
    {
        rigidbody.AddForce(transform.up * jupmForce, ForceMode2D.Impulse);
    }
}