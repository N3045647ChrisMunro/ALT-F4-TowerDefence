﻿using UnityEngine;
using System.Collections;

public class thirdHTower : Tower
{

    void Start()
    {
        this.type = "ThirdHTower";
        this.damage = 25;
        this.range = 3f;
        this.fireCooldown = 1.5f;
        this.canShoot = true;

        this.CurFace = GameObject.Find("PlaneDetector").GetComponent<planeDetector>().currentPlane;
        
        //Audio
        this.shootSnd = GameObject.FindGameObjectWithTag("Audio").GetComponent<inGameAudio>();

        pointOfFire = transform.FindChild("PointOfFire");
    }

    void ShootAt(GameObject e)
    {
        this.canShoot = false;
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, pointOfFire.position, this.transform.rotation);
        Bullet b = bulletGO.GetComponent<Bullet>();
        b.target = e.transform;
        b.damage = damage;
        b.type = "AOE";
        StartCoroutine("shotDelay", fireCooldown);
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            string enemyCurrFace = col.gameObject.GetComponent<EnemyBase>().currFace;

            //Make sure the enemy is on the same face as the turret
            if (CurFace == enemyCurrFace)
            {
                this.nearestEnemy = col.gameObject;

                if (canShoot == true && nearestEnemy != null)
                {
                    Rotation(nearestEnemy);
                    ShootAt(nearestEnemy);
                }
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            nearestEnemy = null;
        }
    }

}
