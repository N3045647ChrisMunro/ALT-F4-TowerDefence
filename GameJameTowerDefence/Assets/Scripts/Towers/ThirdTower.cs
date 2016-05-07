using UnityEngine;
using System.Collections;

public class ThirdTower : Tower
{

    // Use this for initialization
    void Start()
    {
        this.type = "ThirdLightTower";
        this.damage = 15;
        this.range = 4f;
        this.fireCooldown = 0.25f;
        this.fireCooldownLeft = fireCooldown;

        this.CurFace = GameObject.Find("PlaneDetector").GetComponent<planeDetector>().currentPlane;

        pointOfFire = transform.FindChild("PointOfFire");
    }

    // Update is called once per frame
    void Update()
    {
        searchEnemies();
    }
}
