using UnityEngine;
using System.Collections;

public class planeDetection : MonoBehaviour {

    public GameObject greatCube;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	    
	}

    public int faceDetection()
    {
        int face = 0;

        RaycastHit hit;
        Vector3 rayOrigin = new Vector3();
        rayOrigin = this.transform.position;
        Ray cursorRay = new Ray(rayOrigin, Vector3.down);  //Create a ray with cursor as an origin and down as a direction

        if (Physics.Raycast(cursorRay, out hit))                     //If something was hit
        {
            if (hit.collider.name == "TopPlane")                         //Check if it is a tile
            {
                face = 0;
            }

            if (hit.collider.name == "RightPlane")
            {
                face = 1;
            }

            if (hit.collider.name == "LeftPlane")
            {
                face = 2;
            }

            if (hit.collider.name == "NearPlane")
            {
                face = 3;
            }

            if (hit.collider.name == "FarPlane")
            {
                face = 4;
            }

            if (hit.collider.name == "BotPlane")
            {
                face = 5;
            }

        }
        Debug.Log(face);

        return face;
    }
}
