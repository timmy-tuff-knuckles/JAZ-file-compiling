using UnityEngine;
using System.Collections; 
using System.Collections.Generic; 

public class Bullet : MonoBehaviour
{
    [SerializeField] private float normalBulletSpeed = 15f;
    [SerializeField] private float destroyTime = 3f; 
    [SerializeField] private LayerMask whatDestroysBullet; 

    private Rigidbody2D rb; 
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 

        SetDestroyTime(); 
        
        SetStraightVelocity(); 
    }
     

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //This is the collision 
        if((whatDestroysBullet.value & (1 << collision.gameObject.layer)) > 0 )
        {
            //spawn particles

            //play sound FX

            //screenShake

            //Damage Enemy

            //damage enemy
               
            //Destroy the bullet
            Destroy(gameObject); 
        }
    }

    private void SetStraightVelocity()
    {
        rb.linearVelocity = transform.right * normalBulletSpeed;
    }

    private void SetDestroyTime()
    {
        Destroy(gameObject,destroyTime); 
    }
}