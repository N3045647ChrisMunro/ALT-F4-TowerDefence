using UnityEngine;
using System.Collections;

public class SpeedEnemy : EnemyBase {

    private int currentWaypoint = 0;
    Transform targetWaypoint;

    public ScoreSystem scoreSystem;

    // Use this for initialization
    void Start()
    {

        this.transform.SetParent(GameObject.FindGameObjectWithTag("WorldCube").transform, false);

        this.type = "speed";
        this.health = 10;
        this.moveSpeed = 4.0f;

        //This finds the Gameobject in the scene that hold all the waypoints
        this.waypointScript = GameObject.FindGameObjectWithTag("WorldCube");

        //this accesses the script attached to the gameobject
        this.wayPoints = waypointScript.GetComponent<Grid>().waypoints;

        //Initializing Score system
        this.gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ScoreSystem>();

    }

    // Update is called once per frame
    void Update()
    {

        if (currentWaypoint < this.wayPoints.Count)
        {
            if (targetWaypoint == null)
            {
                targetWaypoint = this.wayPoints[currentWaypoint];
            }
        }

        //check to see if we hit the last waypoint
        if (currentWaypoint >= this.wayPoints.Count)
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
            gameManager.UpdateGold = true;
            Destroy(this.gameObject);
        }

        if (currentWaypoint <= 11)
        {
            this.currFace = "TopPlane";
        }
        if (currentWaypoint > 11 && currentWaypoint <= 21)
        {
            this.currFace = "FarPlane";
        }
        if (currentWaypoint > 21 && currentWaypoint <= 37)
        {
            this.currFace = "BotPlane";
        }
        if (currentWaypoint > 38 && currentWaypoint <= 47)
        {
            this.currFace = "NearPlane";
        }

        fixRotation();

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
            if (currentWaypoint == this.wayPoints.Count - 1)
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
            health -= 5;
        }
        if (col.gameObject.tag == "Slow")
        {
            applySlow();
        }
        if (col.gameObject.tag == "AOE")
        {
            health -= 3;
        }
        if (col.gameObject.tag == "Core")
        {
            gameManager.playerHealth--;
        }
    }

}
