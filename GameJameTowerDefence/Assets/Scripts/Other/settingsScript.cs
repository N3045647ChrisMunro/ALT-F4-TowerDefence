using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class settingsScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        float triggerPressed = Input.GetAxis("TriggerAnalogue");        //Get value from analogue
        if (triggerPressed != 0)                                          //If LT is pressed
        {
           SceneManager.LoadScene(0);
        }

    }
}
