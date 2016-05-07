using UnityEngine;
using System.Collections;

public class FirstTower :Tower {

    void Start()
    {
        this.type = "basicLightTower";
        this.damage = 3;
        this.range = 2f;
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
