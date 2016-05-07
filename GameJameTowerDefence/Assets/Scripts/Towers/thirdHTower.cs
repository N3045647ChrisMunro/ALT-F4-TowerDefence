using UnityEngine;
using System.Collections;

public class thirdHTower : Tower
{

    void Start()
    {
        this.type = "ThirdHTower";
        this.damage = 25;
        this.range = 3f;
        this.fireCooldown = 1.5f;
        this.fireCooldownLeft = 0.5f;

        this.CurFace = GameObject.Find("PlaneDetector").GetComponent<planeDetector>().currentPlane;

        pointOfFire = transform.FindChild("PointOfFire");
    }

    void Update()
    {
        searchEnemies();
    }
}
