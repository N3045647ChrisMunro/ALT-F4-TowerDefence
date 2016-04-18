using UnityEngine;
using System.Collections;

public class BasicEnemy : EnemyBase{

    private int currentWaypoint = 0;
    Transform targetWaypoint;

	// Use this for initialization
    void Start()
    {
        this.type = "basic";
        this.health = 10;
        this.moveSpeed = 2.5f;

        this.wayPoints = new Transform[3];

        //TODO: Clean this up
        this.wayPoints[0] = GameObject.Find("1").transform;
        this.wayPoints[1] = GameObject.Find("2").transform;
        this.wayPoints[2] = GameObject.Find("3").transform;

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
            currentWaypoint++;
            targetWaypoint = this.wayPoints[currentWaypoint];

        }

    }

}
