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

        //Restet the rotation so the enemy spawns correctly 
        Quaternion tempRot = new Quaternion(0, 0, 0, 0);
        spawnPoint.transform.rotation = tempRot;

        //Get the tile pos and add a custom y value so the enemy sits onto of the plane
        //and not half inside
        Vector3 tilePos = grid.getTilePosition(17, 3);

        spawnPoint.transform.position = tilePos;

        //Change colour
        grid.ChangeTileColour(Color.black, 17, 3);

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
