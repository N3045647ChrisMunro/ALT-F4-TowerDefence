using UnityEngine;
using System.Collections;

public class menuCubeRotation : MonoBehaviour {

    public float cubeAngle = 30;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(0, 0, cubeAngle * Time.deltaTime); //rotates 50 degrees per second around z axis
    }
}
