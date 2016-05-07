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
        this.fireCooldownLeft = 0.5f;

        this.CurFace = GameObject.Find("PlaneDetector").GetComponent<planeDetector>().currentPlane;

        pointOfFire = transform.FindChild("PointOfFire");
    }

    void Update()
    {
        searchEnemies();
    }

}
