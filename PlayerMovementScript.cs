//PlayerMovementScript
public class PlayerMovementScript : MonoBehaviour
{
    public float movespeed = 5f; // controlls how fast you move

    public Rigidbody2D rb;
    public Vector2 movement; 

    void Start()
    {
        // Get the Rigidbody2D component attached to the sprite
        rb = GetComponent<Rigidbody2D>();
    }
     void Update()
    {
        // Capture input from WASD and Arrow Keys
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }
    void FixedUpdate()
    {
        // Move the Rigidbody using physics
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }    
}