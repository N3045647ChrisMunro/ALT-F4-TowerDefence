using UnityEngine;
using System.Collections;

public class AOE : MonoBehaviour {

	// Use this for initialization

    public float timeInterval = 1f;

    private float currentTime = 0f;

	void Awake () {
        currentTime = timeInterval;

	}
	
	// Update is called once per frame
	void Update () {
	 if (currentTime <=0 )
     {
         Destroy(this.gameObject);

     }
     else
     {
         currentTime -= Time.deltaTime;
     }
	}
}
