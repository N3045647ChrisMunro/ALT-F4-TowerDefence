using UnityEngine;
using System.Collections;

public class settingsGame : MonoBehaviour {

    public bool soundOn = true;

	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(this.gameObject);
	}
	
}
