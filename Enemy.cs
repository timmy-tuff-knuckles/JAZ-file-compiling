using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayerMask; 
    [SerializeField] private float detectionRadius = 3f;
    //Reference to needed locations 
    public List<Transform> points;
    //Interger value for the next point index
    public int nextID = 0;
    //The value of that applies to ID for changing
    private int idValueChange = 1;
    //Speed of movement
    public float enemySpeed;
    public Transform DetectPlayer;
    public Transform playerHitbox;
    private float IsPlayerInRange;
    private float baseScaleX;
    private float baseScaleY;

    bool isPlayerInRange()
    {
        return Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayerMask) != null;
    }

    private void Start()
    {
        baseScaleX = Mathf.Abs(transform.localScale.x);
        baseScaleY = Mathf.Abs(transform.localScale.y);
    }

    private void Update()
    {
        bool isTouching = isPlayerInRange(); 

        if (isTouching)
        {
            if (playerHitbox != null)
            {
                FaceTarget(playerHitbox.position);
                transform.position = Vector2.MoveTowards(transform.position,new Vector2(playerHitbox.position.x, transform.position.y),enemySpeed * Time.deltaTime);
            }
        }
        else
        {
            MoveToNextPoint(); 
        }   
    }

    private void Reset()
    {
        Init();
    }

    void Init()
    {
        //Make box collider trigger
        GetComponent<BoxCollider2D>().isTrigger = true;

        //Create root object
        GameObject root = new GameObject("Root" + name);

        //Reset Position of Root to enemy object
        root.transform.position = transform.position;

        //Set enemy object as child of root
        transform.SetParent(root.transform);

        //Create waypoints object
        GameObject waypoints = new GameObject("Waypoints");
        waypoints.transform.position = root.transform.position; //Root.transform.position makes the position (0,0) locally not globaly - In relation to the enemy

        //Make waypoints object child of root
        waypoints.transform.SetParent(root.transform);

        //Create two points and reset their position to waypoints objects

        //Make the points children of waypoint object 
        GameObject p1 = new GameObject("Point1");
        p1.transform.SetParent(waypoints.transform);
        p1.transform.position = root.transform.position;
 
        GameObject p2 = new GameObject("Point2");
        p2.transform.SetParent(waypoints.transform);
        p2.transform.position = root.transform.position; 

        //Init points list - then add points to it
        points = new List<Transform>();
        points.Add(p1.transform);
        points.Add(p2.transform);
    }

    void MoveToNextPoint()
    {
        //Get the next point transfrom
        Transform nextPoint = points[nextID];

        //Flip the transform to look into the next point's direction
        if(nextPoint.transform.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(-6,6,1);
            }
        else
            {
                transform.localScale = new Vector3(6,6,1);
            }

        //Move the enemy towards the next point - Locks it to y position only
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(nextPoint.position.x, transform.position.y), enemySpeed * Time.deltaTime);

        //Check the distance between enemy and next point to trigger the point
        if(Vector2.Distance(transform.position, nextPoint.position)<1f)
        {
            //Check if enemy is at the end of the line (make change -1)
            if(nextID == points.Count - 1)
            {
                idValueChange = -1;
            }

            //Check if enemy is not at the end of the line (make change +1)
            if(nextID == 0)
            {
                idValueChange = 1;
            }

            //Apply the change on the nextID
            nextID += idValueChange;
        }

    }

    void FaceTarget(Vector3 playerPosition)
    {
        float direction = playerPosition.x > transform.position.x ? -1f : 1f;
        transform.localScale = new Vector3(baseScaleX * direction, baseScaleY, transform.localScale.z);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
