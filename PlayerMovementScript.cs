using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public Rigidbody2D myRigidbody; 
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;


    private float horizontal;
    public float jumpStrength;
    public float moveSpeed;
    private bool isFacingRight = true;


    // Start is called before the first update frame
    void Start()
    {
        
    }

    // Update is called once per every frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.W) && IsGrounded())
        {
            myRigidbody.linearVelocity = new Vector2(myRigidbody.linearVelocityX, jumpStrength);
        }

        if (Input.GetKeyUp(KeyCode.W) && myRigidbody.linearVelocityY > 0f)
        {
            myRigidbody.linearVelocity = new Vector2(myRigidbody.linearVelocityX, myRigidbody.linearVelocityY * 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded())
        {
            myRigidbody.linearVelocity = new Vector2(myRigidbody.linearVelocityX, jumpStrength);
        }

        if (Input.GetKeyUp(KeyCode.UpArrow) && myRigidbody.linearVelocityY > 0f)
        {
            myRigidbody.linearVelocity = new Vector2(myRigidbody.linearVelocityX, myRigidbody.linearVelocityY * 0.5f);
        }

        Flip();

    }


    private void FixedUpdate()
    {
        myRigidbody.linearVelocity = new Vector2(horizontal * moveSpeed, myRigidbody.linearVelocityY);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

}