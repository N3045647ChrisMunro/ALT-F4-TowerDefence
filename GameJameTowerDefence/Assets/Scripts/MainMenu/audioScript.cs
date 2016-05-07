using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class audioScript : MonoBehaviour {

    //Clicking
    public AudioSource clickSource;
    public AudioClip clickSound;

    //Select
    public AudioSource selectSource;
    public AudioClip selectSound;

	// Use this for initialization
	void Start () 
    {
        DontDestroyOnLoad(this.gameObject);
        clickSource = AddAudio(clickSound);
        selectSource = AddAudio(selectSound);
    }
	
	// Update is called once per frame
	void Update () 
    {
        //Destroy if in a game mode
        if (SceneManager.GetActiveScene().buildIndex == 1)
            Destroy(this.gameObject);

    }

    public AudioSource AddAudio(AudioClip clip)
    {
        AudioSource newSource = gameObject.AddComponent<AudioSource>();
        newSource.clip = clip;
        newSource.playOnAwake = false;
        return newSource;
    }
}
