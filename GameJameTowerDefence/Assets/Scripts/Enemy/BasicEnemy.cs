using UnityEngine;
using System.Collections;

public class BasicEnemy : EnemyBase{

    private int currentWaypoint = 0;
    Transform targetWaypoint;

    //KRISTINA was here
    public ScoreSystem scoreSystem;

	// Use this for initialization
    void Start()
    {
        this.type = "basic";
        this.health = 10;
        this.moveSpeed = 2.5f;

        //This finds the Gameobject in the scene that hold all the waypoints
        this.waypointScript = GameObject.Find("WayPoints");

        //this accesses the script attached to the gameobject
        this.wayPoints = waypointScript.GetComponent<Waypoints>().waypoints;

        //Initializing Score system
        scoreSystem = GameObject.FindGameObjectWithTag("Manager").GetComponent<ScoreSystem>();
    }
	
	// Update is called once per frame

    void Update()
    {
       if (currentWaypoint < this.wayPoints.Length)
        {
            if (targetWaypoint == null)
            {
                targetWaypoint = this.wayPoints[currentWaypoint];
            }
        }

        //check to see if we hit the last waypoint
        if (currentWaypoint >= this.wayPoints.Length)
        {
            currentWaypoint = 0;
            Destroy(this.gameObject);
        }
        else
        {
            move();
        }

        if (health <= 0)
        {
            scoreSystem.UpdateGold = true;
            Debug.Log("Enemy died");
            Destroy(this.gameObject);
        }

    }

    public override void move()
    {
        //Rotate to target 
        transform.forward = Vector3.RotateTowards(transform.forward, targetWaypoint.position - transform.position, 4f * Time.deltaTime, 0.0f); 

        //Move towards the next waypoints position
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, this.moveSpeed * Time.deltaTime);

        //Check to see if we reached the waypoint
        if (transform.position == targetWaypoint.position)
        {
            if (currentWaypoint == this.wayPoints.Length - 1)
            {
                targetWaypoint = null;
                currentWaypoint++;
            }
            else
            {
                currentWaypoint++;
                targetWaypoint = this.wayPoints[currentWaypoint];
            }
        }

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            Debug.Log("GOT HIT");
            health -= 5;
        }
    }

}
