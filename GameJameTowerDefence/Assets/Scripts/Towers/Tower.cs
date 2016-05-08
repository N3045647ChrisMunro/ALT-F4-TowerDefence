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
    private bool canShoot_;

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
    public string CurFace
    {
        get { return curface_; }
        set { curface_ = value; }
    }

    public bool canShoot
    {
        get { return canShoot_; }
        set { canShoot_ = value; }
    }

    public GameObject nearestEnemy
    {
        get { return nearestEnemy_; }
        set { nearestEnemy_ = value; }
    }

    public void ShootAt(GameObject e)
    {
        canShoot_ = false;
        // TODO: Fire out the tip!
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, pointOfFire.position, this.transform.rotation);
        Bullet b = bulletGO.GetComponent<Bullet>();
        b.target = e.transform;
        b.damage = damage_;
        StartCoroutine("shotDelay", fireCooldown);
    }

    IEnumerator shotDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        canShoot_ = true;
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

