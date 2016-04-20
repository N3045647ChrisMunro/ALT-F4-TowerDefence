using UnityEngine;
using System.Collections;

public class CubeLayout : MonoBehaviour {

	// Use this for initialization
    public GameObject rightPlane;
    public GameObject leftPlane;
    public GameObject topPlane;
    public GameObject botPlane;
    public GameObject farPlane;
    public GameObject nearPlane;


	void Start () 
    {  
        //-2.09, 2.74, -1.79

        //RIGHT PLANE
        //Rotation
        Quaternion target = Quaternion.Euler(90, 0, 0);
        rightPlane.transform.rotation = target;
        //Position
        Vector3 newPos = new Vector3(0.0f, -0.442f, 4.42f);
        rightPlane.transform.position = newPos;

        //LEFT PLANE
        //Rotation
        target = Quaternion.Euler(90, 180, 0);
        leftPlane.transform.rotation = target;
        //Position
        newPos = new Vector3(4.914f, -0.442f, -0.486f);
        leftPlane.transform.position = newPos;

        //TOP PLANE
        //Rotation
      //  target = Quaternion.Euler(0, 0, 0);
       // topPlane.transform.rotation = target;
        //Position
        newPos = new Vector3(0, 0, 0);
        topPlane.transform.position = newPos;

        //BOT PLANE
        //Rotation
        target = Quaternion.Euler(0, 180, 180);
        botPlane.transform.rotation = target;
        //Position
        newPos = new Vector3(0.0f, -4.815f, 3.926f);
        botPlane.transform.position = newPos;

        //FAR PLANE
        //Rotation
        target = Quaternion.Euler(90, 270, 0);
        farPlane.transform.rotation = target;
        //Position
        newPos = new Vector3(0.044f, -0.442f, -0.485f);
        farPlane.transform.position = newPos;

        //NEAR PLANE
        //Rotation
        target = Quaternion.Euler(90, 90, 0);
        nearPlane.transform.rotation = target;
        //Position
        newPos = new Vector3(4.864f, -0.442f, 4.417f);
        nearPlane.transform.position = newPos;

	}
	
	// Update is called once per frame
	void Update () 
    {
	    
	}
}
