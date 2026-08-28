using UnityEngine;
using Pathfinding;

public class PonaturiAI : MonoBehaviour
{
    public Transform target; // The target the enemy will follow - Player
    public float activateDistance = 10f; // The distance at which the enemy will start following the player
    public float pathUpdateSeconds = 0.5f; // The rate at which the path is updated

    public float speed = 200f;  // The speed at which the enemy moves
    public float nextWaypointDistance = 3f; // The distance at which the enemy will move to the next waypoint in the path
    public float jumpNodeHeightRequirement = 0.8f; // The minimum height difference required for the enemy to jump to the next node
    public float jumpModifier = 0.3f; // The force applied to the enemy when it jumps
    public float jumpCheckOffset = 0.1f; // The offset used to check if the enemy is grounded
    public float jumpTime = 1f; // The time the enemy has to wait before it can jump again

    public bool followEnabled = true; // Whether the enemy should follow the player
    public bool jumpEnabled = true; // Whether the enemy should jump
    public bool directionLookEnabled = true; // Whether the enemy should look in the direction it is moving

    private Path path; // The current path the enemy is following
    private int currentWaypoint = 0; // The current waypoint the enemy is moving towards
    bool isGrounded = false; // Whether the enemy is currently grounded
    Seeker seeker;
    Rigidbody2D myRigidbody; // The Rigidbody2D component of the enemy, used for physics calculations

    public void Start()
    {
        seeker = GetComponent<Seeker>();
        myRigidbody = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
    }

    private void FixedUpdate()
    {
        if (TargetInDistance() && followEnabled)
        {
            PathFollow();
        }

        //Reduces the jump time every frame
        jumpTime = jumpTime - Time.deltaTime;
    }

    private void UpdatePath()
    {
        if (followEnabled && seeker.IsDone())
        {
            //Uses seeker to find a path from the enemy to the player
            seeker.StartPath(myRigidbody.position, target.position, OnPathComplete);
        }
    }

    private void PathFollow() // Makes the enemy follow the path to the player
    {
        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }

        //Checks if the enemy is grounded
        isGrounded = Physics2D.Raycast(transform.position, -Vector2.up, GetComponent<Collider2D>().bounds.extents.y + jumpCheckOffset); 

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - myRigidbody.position).normalized;
        Vector2 force = new Vector2(direction.x, 0f) * speed * Time.deltaTime;

        if (jumpEnabled && isGrounded && jumpTime <= 0) 
        {
            if (direction.y > jumpNodeHeightRequirement)
            {
                myRigidbody.AddForce(Vector2.up * speed * jumpModifier);
                jumpTime = 2f; // Reset the jump time
            }
        }

        myRigidbody.AddForce(force); // Apply the force to the enemy's Rigidbody2D

        float distance = Vector2.Distance(myRigidbody.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
            return;
        }

        if (directionLookEnabled)
        {
            if (myRigidbody.linearVelocity.x > 0.05f)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            
            else if (myRigidbody.linearVelocity.x < -0.05f)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
    }

    private bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, target.position) < activateDistance;
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
}
