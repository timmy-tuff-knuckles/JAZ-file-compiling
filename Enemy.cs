using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayerMask; 
    //Reference to needed locations
    public List<Transform> points;
    //Interger value for the next point index
    public int nextID = 0;
    //The value of that applies to ID for changing
    private int idValueChange = 1;
    //Speed of movement
    public float enemySpeed;
    public Transform DetectPlayer;
    public GameObject playerHitbox;

    private void Start()
    {
        playerHitbox = GameObject.Find("Brit Boi");
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

        //Reset waypoints position to root

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
    
    
    private void Update()
    {
        bool isTouching = Physics2D.OverlapBox(transform.position, transform.localScale, transform.rotation.eulerAngles.z, playerLayerMask) != null;

        if (isTouching)
        {
            
            transform.position = Vector3.MoveTowards(transform.position, playerHitbox.transform.position, enemySpeed * Time.deltaTime);
        }
        else
        {
            MoveToNextPoint(); 
        }
         
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

        //Move the enemy towards the next point
        transform.position = Vector2.MoveTowards(transform.position, nextPoint.position, enemySpeed * Time.deltaTime); 

        //Check the distance between enemy and next point to trigger the point
        if(Vector2.Distance(transform.position, nextPoint.position)<1f)
        {
            //Check if we are at the end of the line (make change -1)
            if(nextID == points.Count - 1)
            {
                idValueChange = -1;
            }

            //Check if we are not at the end of the line (make change +1)
            if(nextID == 0)
            {
                idValueChange = 1;
            }

            //Apply the change on the nextID
            nextID += idValueChange;

        }

    }

}