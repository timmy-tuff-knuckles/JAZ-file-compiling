using UnityEngine;
using System.Collections; 
using System.Collections.Generic; 

public class Bullet : MonoBehaviour
{
    [SerializeField] private float normalBulletSpeed = 15f;
    [SerializeField] private float destroyTime = 3f; 
    [SerializeField] private LayerMask whatDestroysBullet; 
    [SerializeField] private float normalBulletDamage = 1f;  
    [SerializeField] private float physicsBulletSpeed = 17.5f; 
    private Rigidbody2D rb; 

    public enum BulletType
    {
        Normal,
        Physics
    }
    public BulletType bulletType; 
    private float damage;   
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 

        SetDestroyTime(); 
        
        //set velocity based on bullet type

        InitializeBulletStats();


    }
     

    private void InitializeBulletStats()
    {
        if(bulletType == BulletType.Normal)
        {
            SetStraightVelocity();
        }

        else if (bulletType == BulletType.Physics);
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
            IDdamagable idDamagable = collision.gameObject.GetComponent<IDDamagable>(); 
            if (idDamagable != null)
            {
                //damage enemy
                idDamagable.Damage(damage);
            }
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