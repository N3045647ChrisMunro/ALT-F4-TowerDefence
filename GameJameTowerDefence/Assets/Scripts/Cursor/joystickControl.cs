using UnityEngine;
using System.Collections;

/*This script was created for a game jam module by team Alt+F4
  This script should be attached to a cursor game object
  Axis input should be changed in Edit->ProjectSettings->Input*/

public class joystickControl : MonoBehaviour {

    public float sensetivity = 0.5f;  //how tilted should the joystick be, to detect a movement
    public float step = 0.3f;         //by how much the cursor will advance if input is detected

    //These variables are used to define the area, which cursor should never leave
    //Selection cursor
    private float rightMaximumSel = 4.7f;
    private float leftMaximumSel = 0.1f;
    private float topMaximumSel = 4.2f;
    private float botMaximumSel = -0.3f;

    //Menu Cursor
    private float topMaximumMenu = 2.43f;
    private float botMaximumMenu = 0.03f;

    private float rayOffset = -1;    //Used to correctly cast a rat

    private Camera mainCamera;
    public float cameraStep = 0.1f;

    //Cursor seection
    public GameObject selectionCurs;
    public GameObject menuCurs;
    private GameObject currentCurs;

    //Projection
    private GameObject currentTile;

    //On Click detection
    private bool triggerOn = false;
    private bool triggerOff = false;

    //Initial positions
    private Vector3 initialCameraPos;
    private Vector3 initialSelCursPos;
    private Vector3 initialMenCursPos;

    //Turret
    public GameObject turret;
    public GameObject parentCube;


    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        initialCameraPos = mainCamera.transform.position;

        initialMenCursPos = menuCurs.transform.position;
        initialSelCursPos = selectionCurs.transform.position;
        currentCurs = menuCurs;
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
        newPos = currentCurs.transform.position;

        //Calculate new camera position
        //This is used to slightly move the camera when cursor moves
        Vector3 newCamPos = new Vector3();
        newCamPos = mainCamera.transform.position;

        

        //Z and X axisaxis
        if (currentCurs == selectionCurs)
        {
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

            //Projection:
            RaycastHit hit;
            Vector3 rayOrigin = new Vector3();
            rayOrigin = currentCurs.transform.position;
            rayOrigin.y -= rayOffset;
            Ray cursorRay = new Ray(rayOrigin, Vector3.down);  //Create a ray with cursor as an origin and down as a direction

            if (Physics.Raycast(cursorRay, out hit))                     //If something was hit
            {
                if (hit.collider.tag == "Tile")                         //Check if it is a tile
                {
                    GameObject tile = hit.collider.gameObject;           //Place a turret

                    if(currentTile !=tile && currentTile!=null)
                    {
                        Renderer curTileRend = currentTile.GetComponent<Renderer>();
                        curTileRend.material.color = Color.grey;
                        Renderer tileRend = tile.GetComponent<Renderer>();
                        tileRend.material.color = Color.blue;
                    }

                    currentTile = tile;
                }
            }

        }

        //Y AXIS
        if (currentCurs == menuCurs)
        {
            if (cursorVert > sensetivity && !topEdge())           //positive direction
            {
                newPos.y += step;
                newCamPos.y += cameraStep;
            }

            if (cursorVert < -sensetivity && !botEdge())          //negative direction
            {
                newPos.y -= step;
                newCamPos.y -= cameraStep;
            }
        }

        //Apply new position
        currentCurs.transform.position = newPos;
        mainCamera.transform.position = newCamPos;
    }

    //These functions return true, if a cursor is outside allowed area 
    bool rightEdge()
    {
        if (currentCurs.transform.position.x > rightMaximumSel)
            return true;
        return false;
    }

    bool leftEdge()
    {
        if (currentCurs.transform.position.x < leftMaximumSel)
            return true;
        return false;
    }

    bool topEdge()
    {
        if (currentCurs.transform.position.z > topMaximumSel && currentCurs == selectionCurs)
            return true;

        if (currentCurs.transform.position.y > topMaximumMenu && currentCurs == menuCurs)
            return true;
        return false;
    }

    bool botEdge()
    {
        if (currentCurs.transform.position.z < botMaximumSel && currentCurs == selectionCurs)
            return true;
        if (currentCurs.transform.position.y < botMaximumMenu && currentCurs == menuCurs)
            return true;
        return false;
    }

    //Function to select tiles
    void select()
    {
        float triggerPressed = Input.GetAxis("TriggerAnalogue");        //Get value from analogue
        if(triggerPressed !=0)                                          //If LT is pressed
        {
            triggerOn = true;
        }
        else
        {
            if (triggerOn)
                triggerOff = true;
        }

        if(triggerOn && triggerOff)
        {
            if (currentCurs == selectionCurs)
                onSelectCursorClick();

            if (currentCurs == menuCurs)
                onMenuCursorClick();
        }

    }

    void cursorChange()
    {
        if (currentCurs == selectionCurs)
        {
            menuCurs.SetActive(true);
            menuCurs.transform.position = initialMenCursPos;
            currentCurs = menuCurs;
            selectionCurs.SetActive(false);
        }
        else
        {
            selectionCurs.SetActive(true);
            selectionCurs.transform.position = initialSelCursPos;
            currentCurs = selectionCurs;
            menuCurs.SetActive(false);
        }

        triggerOff = false;
        triggerOn = false;
        mainCamera.transform.position = initialCameraPos;
    }

    void onSelectCursorClick()
    {
        RaycastHit hit;
        Vector3 rayOrigin = new Vector3();
        rayOrigin = currentCurs.transform.position;
        rayOrigin.y -= rayOffset;
        Ray cursorRay = new Ray(rayOrigin, Vector3.down);  //Create a ray with cursor as an origin and down as a direction

        if (Physics.Raycast(cursorRay, out hit))                     //If something was hit
        {
            if (hit.collider.tag == "Tile")                         //Check if it is a tile
            {
                GameObject tile = hit.collider.gameObject;           //Place a turret
                Vector3 turretPos = tile.transform.position;
                GameObject newTurret = Instantiate(turret, turretPos, turret.transform.rotation) as GameObject;
                newTurret.transform.parent = parentCube.transform;
                cursorChange();
            }
        }
    }

    void onMenuCursorClick()
    {
        RaycastHit hit;
        Vector3 rayOrigin = new Vector3();
        rayOrigin = currentCurs.transform.position;
        Ray cursorRay = new Ray(rayOrigin, Vector3.forward);  //Create a ray with cursor as an origin and down as a direction

        if (Physics.Raycast(cursorRay, out hit))                     //If something was hit
        {
            if (hit.collider.tag == "GUI")                         //Check if it is a tile
            {
                cursorChange();
            }
        }
    }
}
