using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {

	// Use this for initialization

	public float range = 10f;
	public GameObject bulletPrefab;
    public Transform pointOfFire;

	public int cost = 5;

	float fireCooldown = 0.5f;
	float fireCooldownLeft = 0;

	public float damage = 1;
	public float radius = 0;

    GameObject nearestEnemy; 

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
		// TODO: Optimize this!
		//Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");


		//Enemy nearestEnemy = null;
        //GameObject nearestEnemy = null;
		float dist = Mathf.Infinity;

		foreach(GameObject e in enemies) {
			float d = Vector3.Distance(this.transform.position, e.transform.position);
			if(nearestEnemy == null || d < dist) {
				nearestEnemy = e;
				dist = d;
			}
		}

		if(nearestEnemy == null) {
			Debug.Log("No enemies?");
			return;
		}

		Vector3 dir = nearestEnemy.transform.position - this.transform.position;

		Quaternion lookRot = Quaternion.LookRotation( dir );

		//Debug.Log(lookRot.eulerAngles.y);
		this.transform.rotation = Quaternion.Euler( 0, lookRot.eulerAngles.y, 0 );

		fireCooldownLeft -= Time.deltaTime;
		if(fireCooldownLeft <= 0 && dir.magnitude <= range) {
			fireCooldownLeft = fireCooldown;
			ShootAt(nearestEnemy);
		}

	}

	void ShootAt(GameObject e) {
		// TODO: Fire out the tip!
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, pointOfFire.position, this.transform.rotation);

		Bullet b = bulletGO.GetComponent<Bullet>();
		b.target = e.transform;
		b.damage = damage;
		b.radius = radius;
	}
}
