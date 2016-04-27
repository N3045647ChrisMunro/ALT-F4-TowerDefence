using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour {

    //KRISTINA was here
    public ScoreSystem scoreSystem;

    //TODO: This entire Script could be nicer

    public Grid grid;

    private GameObject spawnPoint;
   
    public GameObject[] enemies;
    public bool spawnNewWave;
    public int numEnemiesToSpawn;

    private float spawnDelay_ = 0.5f;

    private GameObject enemyToSpawn_;
    private int spawnCount_ = 0;

	// Use this for initialization
	void Start () {

        spawnPoint = new GameObject();

        spawnPoint.transform.position = grid.waypoints[0].transform.localPosition;
        spawnPoint.transform.rotation = grid.waypoints[0].transform.rotation;

        //Initializing Score system
        scoreSystem = GameObject.FindGameObjectWithTag("Manager").GetComponent<ScoreSystem>();
	}
	
	// Update is called once per frame
	void Update () {


        if (spawnNewWave == true)
        {
            enemyToSpawn_ = enemies[0];
            StartCoroutine(waveSpawn());
            scoreSystem.UpdateScore = true;
        }

	}

    IEnumerator waveSpawn()
    {
        for (uint i = 0; i < numEnemiesToSpawn; i++)
        {
            Instantiate(enemyToSpawn_, spawnPoint.transform.position, spawnPoint.transform.rotation);
            spawnCount_++;
            spawnNewWave = false;
            yield return new WaitForSeconds(spawnDelay_);
        }
    }

}
