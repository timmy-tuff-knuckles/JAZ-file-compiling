using UnityEngine;
using System.Collections; 
using System.Collections.Generic; 

public class Bullet : MonoBehaviour
{
    [SerializeField] private float normalBulletSpeed = 15f;
    private Rigidbody2D rb; 

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 

        SetStraightVelocity(); 
    }

private void SetStraightVelocity()
    {
        rb.linearVelocity = transform.right * normalBulletSpeed;
    }
}