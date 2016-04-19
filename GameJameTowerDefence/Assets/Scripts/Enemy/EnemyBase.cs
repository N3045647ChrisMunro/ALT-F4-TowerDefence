using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyBase : MonoBehaviour{

    public GameObject waypointScript;

    private string type_;
    private int health_;
    private float moveSpeed_;
    private Transform[] wayPoints_;

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

    public Transform[] wayPoints
    {
        get { return wayPoints_; }
        set { wayPoints_ = value; }
    }

    public EnemyBase() 
    {

    }

    public virtual void move()
    {
        
    }

}
