using UnityEngine;
using System.Collections;

public class inGameAudio : MonoBehaviour {

    //Clicking
    public AudioSource clickSource;
    public AudioClip clickSound;

    //Select
    public AudioSource selectSource;
    public AudioClip selectSound;

    //On Wave
    public AudioSource waveSource;
    public AudioClip waveSound;

    //Death
    public AudioSource deathSource;
    public AudioClip deathSound;

    //Shoot
    public AudioSource shootSource;
    public AudioClip shootSound;

	// Use this for initialization
	void Start () 
    {
        clickSource = AddAudio(clickSound);
        selectSource = AddAudio(selectSound);
        waveSource = AddAudio(waveSound);
        deathSource = AddAudio(deathSound);
        shootSource = AddAudio(shootSound);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public AudioSource AddAudio(AudioClip clip)
    {
        AudioSource newSource = gameObject.AddComponent<AudioSource>();
        newSource.clip = clip;
        newSource.playOnAwake = false;
        return newSource;
    }
}
