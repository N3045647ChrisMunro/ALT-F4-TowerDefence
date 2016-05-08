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
        //RIGHT PLANE
        //Rotation
        Quaternion target = Quaternion.Euler(270, 180, 0);
        rightPlane.transform.rotation = target;
        //Position
        Vector3 newPos = new Vector3(4.667f, -9.61949f, 4.706f);
        rightPlane.transform.localPosition = newPos;
        
        //LEFT PLANE
        //Rotation
        target = Quaternion.Euler(270, 270, 90);
        leftPlane.transform.localRotation = target;
        //Position
        newPos = new Vector3(0.0f, -14.533f, -0.28405f);
        leftPlane.transform.localPosition = newPos;

        //BOT PLANE
        //Rotation
        target = Quaternion.Euler(0, 0, 180);
        botPlane.transform.rotation = target;
        //Position
        newPos = new Vector3(4.6669f, -4.9893f, -24.56252f);
        botPlane.transform.localPosition = newPos;
        
        //FAR PLANE
        //Rotation
        target = Quaternion.Euler(0, 0, 90);
        farPlane.transform.rotation = target;
        //Position
        newPos = new Vector3(-0.1612f, -4.828f, -19.6539f);
        farPlane.transform.localPosition = newPos;

        //NEAR PLANE
        //Rotation
        target = Quaternion.Euler(0, 0, 270);
        nearPlane.transform.rotation = target;
        //Position
        newPos = new Vector3(4.8281f, -0.16129f, -14.74f);
        nearPlane.transform.localPosition = newPos;
        
	}
}
