using UnityEngine;
using Pathfinding; // Import 'Pathfinding' namespace for detection

public class FlyingEnemyAI : MonoBehaviour
{
    public Transform target; // The target the enemy will follow - Player
    public float activateDistance = 10f; // The distance at which the enemy will start following the player
    public float pathUpdateSeconds = 0.5f; // The rate at which the path is updated

    public float speed = 200f;  // The speed at which the enemy moves
    public float nextWaypointDistance = 3f; // The distance at which the enemy will move to the next waypoint in the path
    public bool followEnabled = true; // Whether the enemy should follow the player
    public bool directionLookEnabled = true; // Whether the enemy should look in the direction it is moving

    private Path path; // The current path the enemy is following
    private int currentWaypoint = 0; // The current waypoint the enemy is moving towards
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
    }

    private void UpdatePath()
    {
        if (followEnabled && seeker.IsDone())
        {
            // Uses seeker to find a path from the enemy to the player
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

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - myRigidbody.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        myRigidbody.AddForce(force); // Apply force to the enemy's Rigidbody2D

        float distance = Vector2.Distance(myRigidbody.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++; // Move to the next waypoint in the path
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
