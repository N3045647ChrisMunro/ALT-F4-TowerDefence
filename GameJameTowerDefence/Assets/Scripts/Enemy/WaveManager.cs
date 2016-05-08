using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour {

    //KRISTINA was here 2k16
    public ScoreSystem scoreSystem;

    //TODO: This entire Script could be nicer

    public Grid grid;

    private GameObject spawnPoint;

    public GameObject[] enemies;
    public List<GameObject[]> waves;
    public int waveCount;
    public bool spawnNewWave;
    public bool waveActive;
    public bool canSpawnWave;

    private float spawnDelay_ = 0.5f;

    private GameObject enemyToSpawn_;

    // Use this for initialization
    void Start()
    {

        spawnPoint = new GameObject();

        spawnPoint.transform.position = grid.waypoints[0].transform.localPosition;
        spawnPoint.transform.rotation = grid.waypoints[0].transform.rotation;

        waves = new List<GameObject[]>();
        //Initializing Score system
        scoreSystem = GameObject.FindGameObjectWithTag("Manager").GetComponent<ScoreSystem>();

        canSpawnWave = true;
        waveCount = 1;

        //First 5 wave
        createWave(10, 0, 0);
        createWave(10, 3, 0);
        createWave(8, 5, 2);
        createWave(25, 10, 0);
        createWave(25, 10, 1);

        //5 - 10
        createWave(40, 20, 0);
        createWave(45, 25, 2);
        createWave(60, 40, 8);
        createWave(40, 30, 15);
        createWave(80, 50, 20);

    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawnWave == true && spawnNewWave == true)
        {
           waveActive = true;
           canSpawnWave = false;
           StartCoroutine(waveSpawn(waveCount));
           scoreSystem.UpdateScore = true;
        }
    }

    IEnumerator waveSpawn(int waveNumber)
    {
        GameObject[] waveArray = waves[waveNumber - 1];

        for (uint i = 0; i < waveArray.Length; i++)
        {
            enemyToSpawn_ = waveArray[i];

            Instantiate(enemyToSpawn_, spawnPoint.transform.position, spawnPoint.transform.rotation);
            spawnNewWave = false;

            yield return new WaitForSeconds(spawnDelay_);
        }
        waveCount++;
        waveActive = false;
        canSpawnWave = true;
    }

    public void createWave(int numOfBasic, int numOfSpeed, int numOfStrong)
    {
        int totalWaveSize = numOfBasic + numOfSpeed + numOfStrong;

        //Create the array to hold all enemies for this wave
        GameObject[] waveArray = new GameObject[totalWaveSize];

        int idx = 0;

        //Add the basic enemies to the array
        for (int i = 0; i < numOfBasic; i++)
        {
            waveArray[idx] = enemies[0];
            idx++;
        }
        //Add the speed enemies to the array
        for (int i = 0; i < numOfSpeed; i++)
        {
            waveArray[idx] = enemies[1];
            idx++;
        }
        //Add the strong enemies to the array
        for (int i = 0; i < numOfStrong; i++)
        {
            waveArray[idx] = enemies[2];
            idx++;
        }

        shuffle(waveArray);

        waves.Add(waveArray);
    }

    void shuffle(GameObject[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            GameObject tmp = array[i];
            int r = Random.Range(i, array.Length);
            array[i] = array[r];
            array[r] = tmp;
        }
    }
}
