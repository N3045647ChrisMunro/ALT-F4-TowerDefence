using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour {

    //TODO: This entire Script could be nicer

    public Transform[] spawnPoints;
    public GameObject[] enemies;
    public bool spawnNewWave;
    public int numEnemiesToSpawn;

    private float spawnDelay_ = 0.5f;

    private GameObject enemyToSpawn_;
    private int spawnCount_ = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (spawnNewWave == true)
        {
            enemyToSpawn_ = enemies[0];
            StartCoroutine(waveSpawn());
        }

	}

    IEnumerator waveSpawn()
    {
        for (uint i = 0; i < numEnemiesToSpawn; i++)
        {
            Debug.Log("Got Here");
            Instantiate(enemyToSpawn_, spawnPoints[0].position, spawnPoints[0].rotation);
            spawnCount_++;
            spawnNewWave = false;
            yield return new WaitForSeconds(spawnDelay_);
        }
    }

}
