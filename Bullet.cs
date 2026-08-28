using UnityEngine;
using System.Collections; 
using System.Collections.Generic; 

public class Bullet : MonoBehaviour
{
    [SerializeField] private float normalBulletSpeed = 15f; // Speed of the bullet
    [SerializeField] private float destroyTime = 3f;  // Time in seconds before the bullet is destroyed automatically
    [SerializeField] private LayerMask whatDestroysBullet; // LayerMask to determine what can destroy the bullet 

    private Rigidbody2D myRigidbody; // Reference to the Rigidbody2D component of the bullet
    
    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>(); 

        SetDestroyTime(); 
        
        SetStraightVelocity(); 
    }
     

    private void OnTriggerEnter2D(Collider2D collision) // Called when the bullet collides with another collider
    {
        //This is the collision 
        if((whatDestroysBullet.value & (1 << collision.gameObject.layer)) > 0 )
        {               
            //Destroy the bullet
            Destroy(gameObject); 
        }
    }

    private void SetStraightVelocity() // Sets the velocity of the bullet to move straight in the direction it is facing
    {
        myRigidbody.linearVelocity = transform.right * normalBulletSpeed;
    }

    private void SetDestroyTime() // Sets the time before the bullet is destroyed automatically
    {
        Destroy(gameObject,destroyTime); 
    }
}