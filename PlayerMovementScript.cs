using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public Rigidbody2D myRigidbody; // refrencing the rigidy body asset in player model - allows us to use and manipulate the physics
    [SerializeField] private Transform groundCheck; // refrencing the location function slightly below player - this is to check if the player is allowed to jump or not
    [SerializeField] private LayerMask groundLayer; // refrences the layers in unity - all ground layers have the tag 'Ground' so we can jump on them
    [SerializeField] private TrailRenderer tr; // refrences the trail renderer in unity - this gives us access to manage the dash visual effects

    public float horizontal; // horizontal gets the left/right velocity of player
    public float jumpStrength; // jump height - because it is a public float I can edit in unity instead of opening code editor
    public float moveSpeed; // speed of left/right movement
    public bool isFacingRight = true; // tells us which way the sprite is facing - true if right, false if left
    private bool canDash = true; // this lets us know if the dash option is available - there is a cooldown
    private bool isDashing; // lets us know if the player is currently dashing
    public float dashingTime; // how long each dash will last
    public float dashingPower = 24f; // how far/fast each dash moves player
    private float dashingCooldown = 1f; // how long until next dash

    // Checks if the player is on the ground and can jump then returns data to variable
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 2f, groundLayer);
    }

    // Update is called once per every frame
    void Update()
    {
        // Checks if dashing
        if (isDashing)
        {
            return;
        }

        // Gets horizontal velocity
        horizontal = Input.GetAxisRaw("Horizontal");

        // Makes player jump if they are grounded - W or Up arrow
        if (Input.GetKeyDown(KeyCode.W) && IsGrounded() || Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded())
        {
            myRigidbody.linearVelocity = new Vector2(myRigidbody.linearVelocityX, jumpStrength);
        }

        if (Input.GetKeyUp(KeyCode.W) && myRigidbody.linearVelocityY > 0f || Input.GetKeyUp(KeyCode.UpArrow) && myRigidbody.linearVelocityY > 0f)
        {
            myRigidbody.linearVelocity = new Vector2(myRigidbody.linearVelocityX, myRigidbody.linearVelocityY * 0.5f);
        }

        // Checks if person wants to dash and can dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        Flip();
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        // Moves the player horizontaly when they click A, D, or <, >
        myRigidbody.linearVelocity = new Vector2(horizontal * moveSpeed, myRigidbody.linearVelocityY);
    }

    // Checks if player is moving a direction then flips the model in that direction
    public void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private IEnumerator Dash() // Dash function
    {
        canDash = false;
        isDashing = true;
        float originalGravity = myRigidbody.gravityScale;
        myRigidbody.gravityScale = 0f;
        myRigidbody.linearVelocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        myRigidbody.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}