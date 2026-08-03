using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public Rigidbody2D myRigidbody; 

    private float horizontal;
    public float jumpStrength;
    public float moveSpeed;

    // Start is called before the first update frame
    void Start()
    {
        
    }

    // Update is called once per every frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) == true)
        {
            myRigidbody.linearVelocity = Vector2.up * jumpStrength;
        }


    }

}