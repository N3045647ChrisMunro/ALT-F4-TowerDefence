using UnityEngine;
using System.Collections;

public class SecondSTower : Tower
{

    void Start()
    {
        this.type = "SecondSTower";
        this.damage = 5;
        this.range = 2f;
        this.fireCooldown = 3f;
        this.canShoot = true;

        this.CurFace = GameObject.Find("PlaneDetector").GetComponent<planeDetector>().currentPlane;
        
        //Audio
        this.shootSnd = GameObject.FindGameObjectWithTag("Audio").GetComponent<inGameAudio>();

        pointOfFire = transform.FindChild("PointOfFire");
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
