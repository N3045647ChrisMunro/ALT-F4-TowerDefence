using UnityEngine;
using System.Collections;

public class firstHTower : Tower
{

    void Start()
    {
        this.type = "basicHTower";
        this.damage = 15;
        this.range = 2.2f;
        this.fireCooldown = 2.0f;
        this.fireCooldownLeft = 0.5f;

        this.CurFace = GameObject.Find("PlaneDetector").GetComponent<planeDetector>().currentPlane;

        pointOfFire = transform.FindChild("PointOfFire");
    }

    void Update()
    {
        searchEnemies();
    }

    void ShootAt(GameObject e) 
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, pointOfFire.position, this.transform.rotation);

        Bullet b = bulletGO.GetComponent<Bullet>();
        b.target = e.transform;
        b.damage = damage;
        b.radius = range;
        b.type = "AOE";
    }
}
