using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {

    private float range_;
    private string type_;
    private float fireCooldown_;
    private float fireCooldownLeft_;
    private int damage_;
    private string curface_;

    private GameObject nearestEnemy_;
    public GameObject bulletPrefab;
    public Transform pointOfFire;
    public WaveManager waveManager;

    private Transform towerTransform;
    public Vector3 TowerRotPoint;

    public planeDetector planeDetector;

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
        waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();

        if (waveManager.waveActive == true)
        {
            pickNewEnemy();

            if (nearestEnemy_ == null)
            {
                pickNewEnemy();
            }
            else
            {
                // this.transform.rotation = newRot;
                fireCooldownLeft -= Time.deltaTime;

                float d = Vector3.Distance(this.transform.position, nearestEnemy_.transform.position);

                if (d <= range_)
                {          
                    if (nearestEnemy_.name == "BasicEnemy(Clone)")
                    {
                        if (fireCooldownLeft <= 0 && curface_ == nearestEnemy_.GetComponent<BasicEnemy>().currFace)
                        {
                            fireCooldownLeft = fireCooldown;
                            Rotation(nearestEnemy_);
                            ShootAt(nearestEnemy_);
                        }
                    }
                    if (nearestEnemy_.name == "SpeedEnemy(Clone)")
                    {
                        if (fireCooldownLeft <= 0 && curface_ == nearestEnemy_.GetComponent<SpeedEnemy>().currFace)
                        {
                            fireCooldownLeft = fireCooldown;
                            Rotation(nearestEnemy_);
                            ShootAt(nearestEnemy_);
                        }
                    }
                    if (nearestEnemy_.name == "StrongEnemy(Clone)")
                    {
                        if (fireCooldownLeft <= 0 && curface_ == nearestEnemy_.GetComponent<StrongEnemy>().currFace)
                        {
                            fireCooldownLeft = fireCooldown;
                            Rotation(nearestEnemy_);
                            ShootAt(nearestEnemy_);
                        }
                    }
                }
                else
                {
                    pickNewEnemy();
                }
            }
        }
    }

    void pickNewEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        //Choose closest
        foreach (GameObject e in enemies)
        {
            switch (e.name)
            {
                case "BasicEnemy(Clone)":
                    if (e.GetComponent<BasicEnemy>().currFace == curface_)
                    {
                        nearestEnemy_ = e;
                    }
                    break;

                case "SpeedEnemy(Clone)":
                    if (e.GetComponent<SpeedEnemy>().currFace == curface_)
                    {
                        nearestEnemy_ = e;
                    }
                    break;

                case "StrongEnemy(Clone)":
                    if (e.GetComponent<StrongEnemy>().currFace == curface_)
                    {
                        nearestEnemy_ = e;
                    }
                    break;
            }

        }
    }
    void ShootAt(GameObject e)
    {
        // TODO: Fire out the tip!
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, pointOfFire.position, this.transform.rotation);
        Bullet b = bulletGO.GetComponent<Bullet>();
        b.target = e.transform;
        b.damage = damage_;
        b.radius = range_;
    }

    public void Rotation(GameObject nearestEnemy)
   {
       TowerRotPoint = this.GetComponent<Collider>().bounds.center;
       
       towerTransform = this.transform;

       Vector3 dir = nearestEnemy.transform.position - this.transform.position;
       Quaternion lookRot = Quaternion.LookRotation(dir);

     

       this.planeDetector = GameObject.Find("PlaneDetector").GetComponent<planeDetector>();

       string worldCurrentFace = planeDetector.currentPlane;

       if (CurFace == "TopPlane" && worldCurrentFace == "FarPlane")
       {
           TowerRotPoint.x = lookRot.eulerAngles.y + 90;
           TowerRotPoint.y = 0f;
           TowerRotPoint.z = -90f;
       }
       if (CurFace == "TopPlane" && worldCurrentFace == "NearPlane")
       {
           TowerRotPoint.x = lookRot.eulerAngles.y + -90;
           TowerRotPoint.y = 0f;
           TowerRotPoint.z = 90f;
       }
       if (CurFace == "FarPlane" && worldCurrentFace == "TopPlane")
       {
           TowerRotPoint.x = lookRot.eulerAngles.y + -90;
           TowerRotPoint.y = 0f;
           TowerRotPoint.z = 90f;
       }
       if (CurFace == "FarPlane" && worldCurrentFace == "BotPlane")
       {
           TowerRotPoint.x = lookRot.eulerAngles.y + 90;
           TowerRotPoint.y = 0f;
           TowerRotPoint.z = -90f;
       }
       if (CurFace == "TopPlane" && worldCurrentFace == "BotPlane")
       {
           TowerRotPoint.x = lookRot.eulerAngles.y + 90;
           TowerRotPoint.y = 0f;
           TowerRotPoint.z = -90f;
       }
       if (CurFace == "NearPlane" && worldCurrentFace == "BotPlane")
       {
           TowerRotPoint.x = lookRot.eulerAngles.y + 180;
           TowerRotPoint.y = 0f;
           TowerRotPoint.z = 90f;
       }
       if (CurFace == "BotPlane" && worldCurrentFace == "FarPlane")
       {
           TowerRotPoint.x = -lookRot.eulerAngles.y + -90;
           TowerRotPoint.y = 0f;
           TowerRotPoint.z = 90f;
       }
       else if (CurFace == worldCurrentFace)
       {
           TowerRotPoint.x = 0f;
           TowerRotPoint.y = lookRot.eulerAngles.y + 90;
           TowerRotPoint.z = 0f;
       }

       towerTransform.rotation = Quaternion.Euler(TowerRotPoint);
       
   }
}

