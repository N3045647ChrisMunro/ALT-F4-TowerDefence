﻿using UnityEngine;
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

    //Auudio
    public inGameAudio shootSnd;

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
        //Audio
        shootSnd.shootSource.Play();
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
/*
       if (CurFace == "TopPlane" && worldCurrentFace == "FarPlane")
       {
           //lookRot.eulerAngles.y + 90;
           TowerRotPoint.x = 0f;
           TowerRotPoint.y = lookRot.eulerAngles.y + 90f;
           TowerRotPoint.z = 0f;
       }
       if (CurFace == "TopPlane" && worldCurrentFace == "NearPlane")
       {
           TowerRotPoint.x = 0f;
           TowerRotPoint.y = lookRot.eulerAngles.y + 90f;
           TowerRotPoint.z = 0f;
       }
       if (CurFace == "FarPlane" && worldCurrentFace == "TopPlane")
       {
           TowerRotPoint.x = 0f;
           TowerRotPoint.y = lookRot.eulerAngles.y + 90f;
           TowerRotPoint.z = 0f;
       }
       if (CurFace == "FarPlane" && worldCurrentFace == "BotPlane")
       {
           TowerRotPoint.x = 0f;
           TowerRotPoint.y = lookRot.eulerAngles.y + 90f;
           TowerRotPoint.z = 0f;
       }
       if (CurFace == "TopPlane" && worldCurrentFace == "BotPlane")
       {
           TowerRotPoint.x = 0f;
           TowerRotPoint.y = lookRot.eulerAngles.y + 90f;
           TowerRotPoint.z = 0f;
       }
       if (CurFace == "NearPlane" && worldCurrentFace == "BotPlane")
       {
           TowerRotPoint.x = 0f;
           TowerRotPoint.y = lookRot.eulerAngles.y + 90f;
           TowerRotPoint.z = 0f;
       }
       if (CurFace == "BotPlane" && worldCurrentFace == "FarPlane")
       {
           TowerRotPoint.x = 0f;
           TowerRotPoint.y = lookRot.eulerAngles.y + 90f;
           TowerRotPoint.z = 0f;
       }
       else if (CurFace == worldCurrentFace)
       {
           TowerRotPoint.x = 0f;
           TowerRotPoint.y = lookRot.eulerAngles.y + 90;
           TowerRotPoint.z = 0f;
       }
     */
       if (CurFace == "BotPlane")
       {
           TowerRotPoint.x = 0f;
           TowerRotPoint.y = -lookRot.eulerAngles.y + 90;
           TowerRotPoint.z = 0f;
       }
       else
       {
           TowerRotPoint.x = 0f;
           TowerRotPoint.y = lookRot.eulerAngles.y + 90;
           TowerRotPoint.z = 0f;
       }
       towerTransform.localRotation = Quaternion.Euler(TowerRotPoint);  
   }

}

