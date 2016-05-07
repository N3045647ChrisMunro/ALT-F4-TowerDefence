using UnityEngine;
using System.Collections;

public class ThirdSTower : Tower
{

    void Start()
    {
        this.type = "ThirdSTower";
        this.damage = 7;
        this.range = 2.5f;
        this.fireCooldown = 2.5f;
        this.fireCooldownLeft = 0.5f;

        this.CurFace = GameObject.Find("PlaneDetector").GetComponent<planeDetector>().currentPlane;

        pointOfFire = transform.FindChild("PointOfFire");
    }

    void Update()
    {
        searchEnemies();
    }

}
