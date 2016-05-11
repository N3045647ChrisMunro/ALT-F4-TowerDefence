using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyBase : MonoBehaviour{

    public GameObject waypointScript;

    private string type_;
    private int health_;
    private float moveSpeed_;
    private string currFace_;
    private List<Transform> wayPoints_;
    public ScoreSystem gameManager;

    public planeDetector planeDetector;

    public string type
    {
        get { return type_; }
        set { type_ = value; }
    }

    public int health
    {
        get { return health_; }
        set { health_ = value; }
    }

    public float moveSpeed
    {
        get { return moveSpeed_; }
        set { moveSpeed_ = value; }
    }

    public List<Transform> wayPoints
    {
        get { return wayPoints_; }
        set { wayPoints_ = value; }
    }

    public string currFace
    {
        get { return currFace_; }
        set { currFace_ = value; }
    } 

    public EnemyBase() 
    {
        
    }

    public virtual void move()
    {
        
    }

    public virtual void TakeDamage(int amount)
    {
        health -= amount;
    }

    public virtual void fixRotation()
    {
        this.planeDetector = GameObject.Find("PlaneDetector").GetComponent<planeDetector>();

        string worldCurrentFace = planeDetector.currentPlane;

        if (currFace == "TopPlane" && worldCurrentFace != "TopPlane")
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        }

        if (currFace == "FarPlane" && worldCurrentFace != "FarPlane")
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        }
        if (currFace == "BotPlane" && worldCurrentFace != "BotPlane")
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        }

        if (currFace == "NearPlane" && worldCurrentFace != "NearPlane")
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        }
    }

    public virtual void applySlow()
    {
        moveSpeed = moveSpeed / 2;

        StartCoroutine("timer");
    }

    IEnumerator timer()
    {
        yield return new WaitForSeconds(3);
        moveSpeed = moveSpeed * 2;
    }

    public void resetCurrentFace(int currentWaypoint)
    {
        if (currentWaypoint <= 12)
        {
            this.currFace = "TopPlane";
        }
        if (currentWaypoint > 12 && currentWaypoint <= 21)
        {
            this.currFace = "FarPlane";
        }
        if (currentWaypoint > 21 && currentWaypoint <= 36)
        {
            this.currFace = "LeftPlane";
        }
        if (currentWaypoint > 36 && currentWaypoint <= 48)
        {
            this.currFace = "BotPlane";
        }
        if (currentWaypoint > 48 && currentWaypoint <= 63)
        {
            this.currFace = "RightPlane";
        }
        if (currentWaypoint > 63 && currentWaypoint <= 71)
        {
            this.currFace = "NearPlane";
        }
    }

}
