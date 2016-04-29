using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {

    private float range_;
    private string type_;
    private float fireCooldown_;
    private float fireCooldownLeft_;
    private int damage_;
    private string curface_;

    public GameObject nearestEnemy;
    public GameObject bulletPrefab;
    public Transform pointOfFire;

    public string type
    {
        get { return type_; }
        set { type_ = value; }
    }

    public float range
    {
        get { return range_; }
        set { range_ = value; }
    }

    public int damage
    {
        get { return damage_; }
        set { damage_ = value; }
    }

    public float fireCooldown
    {
        get { return fireCooldown_; }
        set { fireCooldown_ = value; }
    }
    public float fireCooldownLeft
    {
        get { return fireCooldownLeft_; }
        set { fireCooldownLeft_ = value; }
    }
    public string CurFace
    {
        get { return curface_; }
        set { curface_ = value; }
    }

    public void searchEnemies()
    {
        /*GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        //float dist = 5.0f;

        //Choose closest
        foreach (GameObject e in enemies)
        {
            float d = Vector3.Distance(this.transform.position, e.transform.position);
            if (nearestEnemy == null || d < range && e.GetComponent<BasicEnemy>().currFace == curface_)
            {
                nearestEnemy = e;
                range = d;
            }
        }

        //If enemies exist
        if (nearestEnemy == null)
        {
            return;
        }
        */

        pickNewEnemy();

        //Vector3 dir = nearestEnemy.transform.position - this.transform.position;

        if (nearestEnemy == null)
        {
            pickNewEnemy();
        }

       // this.transform.rotation = newRot;
        fireCooldownLeft -= Time.deltaTime;

        //&& current face == enenmy current face

        if (nearestEnemy.name == "BasicEnemy(Clone)")
        {
            Debug.Log("BASIC ENEMY IS NEAREST");


            if (fireCooldownLeft <= 0 && curface_ == nearestEnemy.GetComponent<BasicEnemy>().currFace)
            {
                fireCooldownLeft = fireCooldown;
                //this.transform.Rotate(size, 90); //Quaternion.Euler(0, lookRot.eulerAngles.y, 0);
                ShootAt(nearestEnemy);
                Debug.Log("SHOOT");
            }
        }
        if (nearestEnemy.name == "SpeedEnemy(Clone)")
        {

            Debug.Log("SPEED ENEMY IS NEAREST");

            if (fireCooldownLeft <= 0 && curface_ == nearestEnemy.GetComponent<SpeedEnemy>().currFace)
            {
                fireCooldownLeft = fireCooldown;
                //this.transform.Rotate(size, 90); //Quaternion.Euler(0, lookRot.eulerAngles.y, 0);
                ShootAt(nearestEnemy);
                Debug.Log("SHOOT");
            }
        }
        if (nearestEnemy.name == "StrongEnemy(Clone)")
        {

            Debug.Log("STRONG ENEMY IS NEAREST");

            if (fireCooldownLeft <= 0 && curface_ == nearestEnemy.GetComponent<StrongEnemy>().currFace)
            {
                fireCooldownLeft = fireCooldown;
                //this.transform.Rotate(size, 90); //Quaternion.Euler(0, lookRot.eulerAngles.y, 0);
                ShootAt(nearestEnemy);
                Debug.Log("SHOOT");
            }
        }

    }

    void pickNewEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        //float dist = 5.0f;

        //Choose closest
        foreach (GameObject e in enemies)
        {
            float d = Vector3.Distance(this.transform.position, e.transform.position);
            
            switch(e.name){
                case "BasicEnemy(Clone)":
                    if (e.GetComponent<BasicEnemy>().currFace == curface_)
                    {
                        nearestEnemy = e;
                        range = d;
                    }
                break;

                case "SpeedEnemy(Clone)":
                    if (e.GetComponent<SpeedEnemy>().currFace == curface_)
                    {
                        nearestEnemy = e;
                        range = d;
                    }
                    break;

                case "StrongEnemy(Clone)":
                    if (e.GetComponent<StrongEnemy>().currFace == curface_)
                    {
                        nearestEnemy = e;
                        range = d;
                    }
                    break;

            }
            
        }
    }

    void ShootAt(GameObject e)
    {
        // TODO: Fire out the tip!
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, pointOfFire.position, this.transform.rotation);
        Debug.Log("Shot a Bullet");
        Bullet b = bulletGO.GetComponent<Bullet>();
        b.target = e.transform;
        b.damage = damage_;
        b.radius = range_;
    }
   
}
