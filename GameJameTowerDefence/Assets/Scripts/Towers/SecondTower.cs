using UnityEngine;
using System.Collections;

public class SecondTower : Tower {

	// Use this for initialization
	void Start () {
        this.type = "SecondLightTower";
        this.damage = 10;
        this.range =  15.0f;
        this.fireCooldown = 0.25f;
        this.fireCooldownLeft = fireCooldown;

        this.CurFace = GameObject.Find("PlaneDetector").GetComponent<planeDetector>().currentPlane;

        pointOfFire = transform.FindChild("PointOfFire");
	}
	
	// Update is called once per frame
	void Update () {
        searchEnemies();
	}
}
