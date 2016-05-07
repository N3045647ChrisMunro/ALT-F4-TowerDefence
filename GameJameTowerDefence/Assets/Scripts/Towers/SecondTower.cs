using UnityEngine;
using System.Collections;

public class SecondTower : Tower {

	// Use this for initialization
	void Start () {
        this.type = "SecondLightTower";
        this.damage = 8;
        this.range =  3f;
        this.fireCooldown = 0.35f;
        this.fireCooldownLeft = fireCooldown;

        this.CurFace = GameObject.Find("PlaneDetector").GetComponent<planeDetector>().currentPlane;

        pointOfFire = transform.FindChild("PointOfFire");
	}
	
	// Update is called once per frame
	void Update () {
        searchEnemies();
	}
}
