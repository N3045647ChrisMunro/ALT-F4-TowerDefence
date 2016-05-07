using UnityEngine;
using System.Collections;

public class applySettings : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        GameObject options = GameObject.FindGameObjectWithTag("Settings");
        if(options!=null)
        {
            //Sound On
            settingsGame settingScrip = options.GetComponent<settingsGame>();
            if(!settingScrip.soundOn)
            {
                GameObject audioS = GameObject.FindGameObjectWithTag("Audio");
                audioS.SetActive(false);
            }
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
