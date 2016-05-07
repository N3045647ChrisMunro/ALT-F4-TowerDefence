using UnityEngine;
using System.Collections;

public class FirstSTower : Tower
{

    void Start()
    {
        this.type = "FirstSTower";
        this.damage = 3;
        this.range = 1.5f;
        this.fireCooldown = 3.5f;
        this.fireCooldownLeft = 0.5f;

        this.CurFace = GameObject.Find("PlaneDetector").GetComponent<planeDetector>().currentPlane;

        pointOfFire = transform.FindChild("PointOfFire");
    }

    void Update()
    {
        searchEnemies();
    }
}
