//PlayerMovementScript
//public class PlayerMovementScript : MonoBehaviour
//{
//    public float movespeed = 5f; // controlls how fast you move
//
//  public Rigidbody2D rb;
//    public Vector2 movement; 
//
//    void Start()
//    {
//        // Get the Rigidbody2D component attached to the sprite
//        rb = GetComponent<Rigidbody2D>();
//   }
//     void Update()
//    {
//        // Capture input from WASD and Arrow Keys
//        movement.x = Input.GetAxisRaw("Horizontal");
//        movement.y = Input.GetAxisRaw("Vertical");
//    }
//    void FixedUpdate()
//    {
//        // Move the Rigidbody using physics
//        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
//    }    
//}



using System.Collection;
using System.Collection.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public RigidBody2D myRigidbody;
    void Start()
    {
        
    }

    void update()
    {
        if (input.getkeydown(keycode.W) == true)
        {
            myRigidbody.velocity = Vector2.up * 10;
        }
         if (input.getkeydown(keycode.A) == true)
        {
            myRigidbody.velocity = Vector2.Left * 10;
        }
         if (input.getkeydown(keycode.D) == true)
        {
            myRigidbody.velocity = Vector2.right * 10;
        }

        
    }





}