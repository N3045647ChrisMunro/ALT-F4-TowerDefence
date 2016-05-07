using UnityEngine;
using System.Collections;

public class secondHTower : Tower
{

    void Start()
    {
        this.type = "secondHTower";
        this.damage = 20;
        this.range = 2.5f;
        this.fireCooldown = 1.75f;
        this.fireCooldownLeft = 0.5f;

        this.CurFace = GameObject.Find("PlaneDetector").GetComponent<planeDetector>().currentPlane;

        pointOfFire = transform.FindChild("PointOfFire");
    }

    void Update()
    {
        searchEnemies();
    }
}
