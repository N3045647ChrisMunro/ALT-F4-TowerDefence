using UnityEngine;
using System.Collections;

public class FirstTower :Tower {

    public string turretType;

    void Start()
    {
        this.type = "Level1_Basic_Turret";
        this.damage = 3;
        this.range = 2f;
        this.fireCooldown = 0.6f;
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
