using UnityEngine;
using System.Collections;

/*This script was created for a game jam module by team Alt+F4
  This script should be attached to a cursor game object
  Axis input should be changed in Edit->ProjectSettings->Input*/

public class joystickControl : MonoBehaviour {

    public float sensetivity = 0.5f;  //how tilted should the joystick be, to detect a movement
    public float step = 0.3f;         //by how much the cursor will advance if input is detected

    //These variables are used to define the area, which cursor should never leave
    private float rightMaximum = 4.7f;
    private float leftMaximum = 0.1f;
    private float topMaximum = 4.2f;
    private float botMaximum = -0.3f;

    private float rayOffset = -1;    //Used to correctly cast a rat

    private Camera mainCamera;
    public float cameraStep = 0.1f;

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

	// Update is called once per frame
	void Update ()
    {
        moveCursor();
        select();
	}

    //This functions moves the cursor accroding to joystick input
    void moveCursor()
    {
        float cursorHor = 0;
        float cursorVert = 0;

        //Get new iput values
        cursorHor = Input.GetAxisRaw("Horizontal");
        cursorVert = Input.GetAxisRaw("Vertical");

        //Calculate new cursor position
        Vector3 newPos = new Vector3();
        newPos = this.transform.position;

        //Calculate new camera position
        //This is used to slightly move the camera when cursor moves
        Vector3 newCamPos = new Vector3();
        newCamPos = mainCamera.transform.position;

        //Sensetivity
        //X axis
        if (cursorHor > sensetivity && !rightEdge())              //positive direction
        {
            newPos.x += step;
            newCamPos.x += cameraStep;
        }

        if (cursorHor < -sensetivity && !leftEdge())           //negative durection
        {
            newPos.x -= step;
            newCamPos.x -= cameraStep;
        }

        //Z axis
        if (cursorVert > sensetivity && !topEdge())           //positive direction
        {
            newPos.z += step;
            newCamPos.z += cameraStep;
        }

        if (cursorVert < -sensetivity && !botEdge())          //negative direction
        {
            newPos.z -= step;
            newCamPos.z -= cameraStep;
        }

        //Apply new position
        transform.position = newPos;
        mainCamera.transform.position = newCamPos;
    }

    //These functions return true, if a cursor is outside allowed area 
    bool rightEdge()
    {
        if (transform.position.x > rightMaximum)
            return true;
        return false;
    }

    bool leftEdge()
    {
        if (transform.position.x < leftMaximum)
            return true;
        return false;
    }

    bool topEdge()
    {
        if (transform.position.z > topMaximum)
            return true;
        return false;
    }

    bool botEdge()
    {
        if (transform.position.z < botMaximum)
            return true;
        return false;
    }

    //Function to select tiles
    void select()
    {
        float triggerPressed = Input.GetAxis("TriggerAnalogue");        //Get value from analogue
        Debug.Log(triggerPressed);
        if(triggerPressed !=0)                                          //If LT is pressed
        {
            RaycastHit hit;
            Vector3 rayOrigin = new Vector3();
            rayOrigin = transform.position;
            rayOrigin.y -= rayOffset;
            Ray cursorRay = new Ray(rayOrigin, Vector3.down);  //Create a ray with cursor as an origin and down as a direction

            //Debug.DrawRay(rayOrigin, Vector3.down);

           if(Physics.Raycast(cursorRay, out hit))                     //If something was hit
            {
                if (hit.collider.tag == "Tile")                         //Check if it is a tile
                {
                    GameObject tile = hit.collider.gameObject;           //HERE ACCORDING CODE GOES
                    Renderer rend = tile.GetComponent<Renderer>();
                    rend.material.color = Color.blue;
                }
            } 
        }

    }
}
