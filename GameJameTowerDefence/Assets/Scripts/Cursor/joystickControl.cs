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
    private float rightMaximumSel = 6.8f;
    private float leftMaximumSel = 2.4f;
    private float topMaximumSel = 6.1f;
    private float botMaximumSel = 1.8f;

    //Menu Cursor
    private float topMaximumMenu = 2.43f;
    private float botMaximumMenu = -1f;

    private Camera mainCamera;
    public float cameraStep = 0.1f;

    //Cursor seection
    public GameObject selectionCurs;
    public GameObject menuCurs;
    private GameObject currentCurs;

    public GameObject cursorProjection;
    public GameObject illegalProjection;

    public GameObject currentTile;
    public GameObject selParticles;
    public GameObject currentTurret;

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
    public GameObject upgradeTurret;

    //Score
    public ScoreSystem scoreSystem;

    //Timer
    public float curTime = 0;
    public float timeInterval = 0.1f;

    //Cursor functionality
    private bool selectingTurret = false;

    //Waves
    public WaveManager waveManager;

    //Turret Menu
    turretMenu towerMenu;
    public GameObject subMenu;
    private bool menuActive = false;

    //Audio
    public GameObject audioMangr;

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        initialCameraPos = mainCamera.transform.position;

        initialMenCursPos = menuCurs.transform.position;
        initialSelCursPos = selectionCurs.transform.position;

        currentCurs = menuCurs;

        //Score system
        scoreSystem = GameObject.FindGameObjectWithTag("Manager").GetComponent<ScoreSystem>();

        //Turret menu
        towerMenu = GetComponent<turretMenu>();

        //Audio
        audioMangr = GameObject.FindGameObjectWithTag("Audio");

    }

	// Update is called once per frame
	void Update ()
    {
        waveManager.spawnNewWave = false;
        if (curTime <= 0.2f && currentCurs!=null)
        {
            moveCursor();
            curTime = timeInterval;
        }
        else
        {
            curTime -= Time.deltaTime;
        }
        //moveMenuCurs();
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

            projectionUpdate();
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

   
    //Manages the projection
    void projectionUpdate()
    {
        RaycastHit hit;
        Vector3 rayOrigin = new Vector3();
        rayOrigin = currentCurs.transform.position;
        //rayOrigin.y -= rayOffset;
        Ray cursorRay = new Ray(rayOrigin, Vector3.down);  //Create a ray with cursor as an origin and down as a direction


        if (Physics.Raycast(cursorRay, out hit))                     //If something was hit
        {
            //OVER TILE
            if (hit.collider.tag == "Tile")                         //Check if it is a tile
            {
                deleteProjection();
                selParticles.SetActive(false);

                GameObject tile = hit.collider.gameObject;
                Vector3 newPos = tile.transform.position;
                newPos.y += 0.07f;
                GameObject newProj = Instantiate(cursorProjection, newPos, cursorProjection.transform.rotation) as GameObject;

                Renderer rend = newProj.GetComponent<Renderer>();
                rend.sharedMaterial.color = Color.green;

                currentTile = tile;
            }

            //OVER ILLEGAL
            if (hit.collider.tag == "UsedTile")
            {
                deleteProjection();
                selParticles.SetActive(false);

                GameObject tile = hit.collider.gameObject;
                Vector3 newPos = tile.transform.position;
                newPos.y += 0.07f;
                GameObject newProj = Instantiate(cursorProjection, newPos, cursorProjection.transform.rotation) as GameObject;

                Renderer rend = newProj.GetComponent<Renderer>();
                rend.sharedMaterial.color = Color.red;

                currentTile = null;
            }

            //OVER TURRET
            if(hit.collider.tag=="Turret" && selectingTurret)
            {
                deleteProjection();
                GameObject Turret = hit.collider.gameObject;
                selParticles.SetActive(true);
                currentTurret = hit.collider.gameObject;
            }

        }
    }

    void deleteProjection()
    {
        GameObject previousProj = GameObject.FindGameObjectWithTag("CursorProjection");

        if (previousProj != null)
        {
            Destroy(previousProj);
        }
    }

    //-------------------------------------------CHECK EDGES-----------------------------------------------
    //These functions return true, if a cursor is outside allowed area 
    bool rightEdge()
    {
        if (currentCurs.transform.position.x > rightMaximumSel)
            return true;
        return false;
    }

    public void resetSelCursor()
    {
        selectionCurs.transform.position = initialSelCursPos;
        mainCamera.transform.position = initialCameraPos;
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
    //-------------------------------------------CHECK EDGES-----------------------------------------------

    //SELECT
    //Reacts on RT pressed
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
            audioMangr = GameObject.FindGameObjectWithTag("Audio");
            //Sound
            if (audioMangr.activeSelf)
            {
                inGameAudio gameAudio = audioMangr.GetComponent<inGameAudio>();
                gameAudio.selectSource.Play();
            }

            //Functionality
            if (menuActive)                         //sub menu
                onTurretSelect();
            else
            {
                if (currentCurs == selectionCurs)
                    onSelectCursorClick();

                if (currentCurs == menuCurs)
                    onMenuCursorClick();
            }
            
        }

    }

    //CURSOR CHANGE
    void cursorChange()
    {
        deleteProjection();
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
        deleteProjection();
    }

    //SELECTION CURSOR ACTIVE
    void onSelectCursorClick()
    {
        if (scoreSystem.buyTurret())
        {
            if (!selectingTurret && currentTile != null)
            {
                GameObject newTurret = Instantiate(turret, currentTile.transform.position, turret.transform.rotation) as GameObject;

                currentTile.tag = "UsedTile";

                turretOffset offset = newTurret.GetComponent<turretOffset>();
                Vector3 turretPos = currentTile.transform.position;
                turretPos.x += offset.xOffset;
                turretPos.z -= offset.zOffset;
                float sizeY = newTurret.GetComponent<BoxCollider>().bounds.size.y;
                turretPos.y += sizeY;

                newTurret.transform.parent = parentCube.transform;
                newTurret.transform.position = turretPos;

                deleteProjection();
                cursorChange();

                currentTile = null;
            }

            if(selectingTurret)
            {
                deleteProjection();
                if (currentTurret != null)
                {
                    Transform prevTransform = currentTurret.transform;
                    turretOffset upgrManager = currentTurret.GetComponent<turretOffset>();
                    GameObject newTurr = upgrManager.UpgradeTurret(prevTransform);

                    if (newTurr != null)
                    {
                        newTurr.transform.parent = parentCube.transform;
                        Destroy(currentTurret);
                    }
                }
                cursorChange();
            }
        }
        else
        {
            cursorChange();
        }
    }

    //MENU CURSOR ACTIVE
    void onMenuCursorClick()
    {
        deleteProjection();
        RaycastHit hit;
        Vector3 rayOrigin = new Vector3();
        rayOrigin = currentCurs.transform.position;
        Ray cursorRay = new Ray(rayOrigin, Vector3.forward);  //Create a ray with cursor as an origin and down as a direction

        //Some button was hit
        if (Physics.Raycast(cursorRay, out hit))                     //If something was hit
        {
            if (hit.collider.tag == "GUI")                         //Check if it is a tile
            {
                //BUY TURRET
                if (hit.collider.name == "buyTurret")
                {
                    if (!menuActive)
                    {
                        menuActive = true;
                        subMenu.SetActive(true);
                        currentCurs = null;
                        menuCurs.transform.position = initialMenCursPos;
                    }
                    selectingTurret = false;
                         
                }

                //UPGRADE TURRET
                if (hit.collider.name == "upgrade")
                {
                    selectingTurret = true;
                    cursorChange();
                }

                //WAVE SPAWN
                if (hit.collider.name == "waveSpawn")
                {
                    waveManager.spawnNewWave = true;

                    //Sound
                    audioMangr = GameObject.FindGameObjectWithTag("Audio");
                    if (audioMangr.activeSelf)
                    {
                        inGameAudio gameAudio = audioMangr.GetComponent<inGameAudio>();
                        gameAudio.waveSource.Play();
                    }
                }
            }
        }
        else
        {
            triggerOff = false;
            triggerOn = false;
        }

        triggerOff = false;
        triggerOn = false;
    }

    void onTurretSelect()
    {
        menuActive = false;
        subMenu.SetActive(false);
        currentCurs = menuCurs;

        triggerOff = false;
        triggerOn = false;

        turret = towerMenu.getTurret();
        cursorChange();
    }

}
