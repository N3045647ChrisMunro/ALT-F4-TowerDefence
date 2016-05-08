using UnityEngine;
using System.Collections;

public class WorldCubeRotation : MonoBehaviour {

    public float sensetivity = 0.7f;
    
    
    private float rotationX = 0;
    private float rotationZ = 0;

    private float delayTime = 0.4f;
    private float currentTime = 0.4f;

    public GameObject cursor;
    private Vector3 cursorPos = new Vector3();

    public planeDetector planeDet;

    public int currentPlane = 0;

    public joystickControl cursorScript;

	// Use this for initialization
	void Start () 
    {
        currentTime = delayTime;
        cursorPos.x = 3.19f;
        cursorPos.y = 0.64f;
        cursorPos.z = 0;
	}
	
	// Update is called once per frame
	void Update () 
    {
        rotateCube();

        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            setCubeRotation();
        }
        /*
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("DOWN ARROW");


            Vector3 rotPos = new Vector3(4.79f, -4.94f, 4.112f);


            transform.RotateAround(rotPos, Vector3.forward, -90);

        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("UP ARROW");

            Vector3 pos = new Vector3(4.79f, -4.94f, 4.13f);

            transform.RotateAround(pos, Vector3.forward, 90);

            //Quaternion angle = Quaternion.Euler(currentAngleX, 0, currentAngleZ);
            //transform.RotateAround(transform.position, Vector3.forward, 90f);
        }*/

	}

    void rotateCube()
    {
        //ROTATION IN X AXIS
        rotationX = Input.GetAxis("RotationVertical");
        rotationZ = Input.GetAxis("RotationHorizontal");
    }


    void setCubeRotation()
    {
        //Positive Rotation
        if (rotationZ > sensetivity)
        {
            Vector3 rotPos = new Vector3(4.79f, -4.94f, 4.112f);
            transform.RotateAround(rotPos, Vector3.forward, 90);
            cursorScript.resetSelCursor();
         }

        //Negative Rotation
         if (rotationZ < -sensetivity)
         {
            Vector3 rotPos = new Vector3(4.79f, -4.94f, 4.112f);
            transform.RotateAround(rotPos, Vector3.back, 90);
            cursorScript.resetSelCursor();
          }

         currentTime += delayTime;
    }
}
