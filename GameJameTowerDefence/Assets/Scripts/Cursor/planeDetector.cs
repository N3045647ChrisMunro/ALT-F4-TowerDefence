using UnityEngine;
using System.Collections;

public class planeDetector : MonoBehaviour
{

    //Kristina testing stuff
    public GameObject world;

    public string currentPlane;

    // Use this for initialization
    void Start()
    {
        //currentPlane = "";
    }

    // Update is called once per frame
    void Update()
    {
        currentPlane = faceDetection();
    }

    public string faceDetection()
    {
        RaycastHit hit;
        Vector3 rayOrigin = new Vector3();
        Vector3 direction = world.transform.position - transform.position;
        rayOrigin = this.transform.position;

        Ray cursorRay = new Ray(rayOrigin, direction);  //Create a ray with cursor as an origin and down as a direction
        

        if (Physics.Raycast(cursorRay, out hit))                     //If something was hit
        {
            if (hit.collider.tag == "TopPlane")                         //Check if it is a tile
            {
                return "TopPlane";
            }
            if (hit.collider.tag == "RightPlane")
            {
                return "RightPlane";
            }
            if (hit.collider.tag == "LeftPlane")
            {
                return "LeftPlane";
            }
            if (hit.collider.tag == "NearPlane")
            {
                return "NearPlane";
            }
            if (hit.collider.tag == "FarPlane")
            {
                return "FarPlane";
            }
            if (hit.collider.tag == "BotPlane")
            {
                return "BotPlane";
            }
            else
            {
                return "UnknownPlane";
            }

        }
        return "";
    }
}