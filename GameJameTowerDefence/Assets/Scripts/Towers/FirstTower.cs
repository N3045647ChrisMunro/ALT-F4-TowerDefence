using UnityEngine;
using System.Collections;

public class FirstTower :Tower {

    void Start()
    {
        this.type = "basicLightTower";
        this.damage = 5;
        this.range = 2.0f;
        this.fireCooldown = 0.5f;
        this.fireCooldownLeft = fireCooldown;

        this.CurFace = GameObject.Find("PlaneDetector").GetComponent<planeDetector>().currentPlane;

        pointOfFire = transform.FindChild("PointOfFire");
    }

    void Update()
    {
        searchEnemies();
    }

  
}
