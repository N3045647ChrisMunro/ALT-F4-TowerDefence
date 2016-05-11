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

    //Coordinates
    private int curX = 0;
    private int curZ = 0;


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

    //public GameObject currentTile;
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
    //public WaveManager waveManager;

    //Turret Menu
    turretMenu towerMenu;
    public GameObject subMenu;
    private bool menuActive = false;

    //Audio
    public GameObject audioMangr;

    //Plane
    public planeDetector planeDet;
    public Grid gridScript;

    //Stuff
    private GameObject curTile;
    private GameObject prevTile;
    private Color originalMat;

    //Indexing
    private int XIndex;
    private int ZIndex;
    public indexDetect indexScript;

    //Main menu
    public menuCursScript selMenu;
    private bool selMenuActive = true;

    public bool buyTurretHit = false;
    public bool upgradeTurretHit = false;

    void Start()
    {
        // mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        // initialCameraPos = mainCamera.transform.position;

        initialMenCursPos = menuCurs.transform.position;
        initialSelCursPos = selectionCurs.transform.position;

        currentCurs = menuCurs;

        //Score system
        scoreSystem = GameObject.FindGameObjectWithTag("Manager").GetComponent<ScoreSystem>();

        //Turret menu
        towerMenu = GetComponent<turretMenu>();

        //Audio
        audioMangr = GameObject.FindGameObjectWithTag("Audio");

        //Indexing
        indexScript = GameObject.FindGameObjectWithTag("indexing").GetComponent<indexDetect>();

        //Sel Menu
        selMenu = GetComponent<menuCursScript>();
    }

    // Update is called once per frame
    void Update()
    {

        if (curTime <= 0.2f && currentCurs != null)
        {
            moveCursor();
            curTime = timeInterval;
        }
        else
        {
            curTime -= Time.deltaTime;
        }

        select();
    }

    //This functions moves the cursor accroding to joystick input
    void moveCursor()
    {
        //Calculate new cursor position
        Vector3 newPos = new Vector3();
        newPos = currentCurs.transform.position;

        //Z and X axisaxis
        if (currentCurs == selectionCurs)
        {
            if (indexScript.currentXIndex == 0 && indexScript.currentZIndex == 0)
                moveFromOrigin();

            if (indexScript.currentXIndex == 9 && indexScript.currentZIndex == 9)
                moveFromEnd();

            if (indexScript.currentXIndex == 0 && indexScript.currentZIndex == 9)
                moveFromRight();

            if (indexScript.currentXIndex == 9 && indexScript.currentZIndex == 0)
                moveFromLeft();

            otherProjection();
        }

    }

    void moveFromOrigin()
    {
        float cursorHor = 0;
        float cursorVert = 0;

        //Get new iput values
        cursorHor = Input.GetAxisRaw("Horizontal");
        cursorVert = Input.GetAxisRaw("Vertical");

        if (cursorVert > sensetivity && curZ < 9)           //positive direction
        {
            curZ++;
        }

        if (cursorVert < -sensetivity && curZ > 0)          //negative direction
        {
            curZ--;
        }

        //X axis
        if (cursorHor > sensetivity && curX < 9)              //positive direction
        {
            curX++;
        }

        if (cursorHor < -sensetivity && curX > 0)           //negative durection
        {
            curX--;
        }
    }

    void moveFromEnd()
    {
        float cursorHor = 0;
        float cursorVert = 0;

        //Get new iput values
        cursorHor = Input.GetAxisRaw("Horizontal");
        cursorVert = Input.GetAxisRaw("Vertical");

        if (cursorVert > sensetivity && curZ > 0)           //positive direction
        {
            curZ--;
        }

        if (cursorVert < -sensetivity && curZ < 9)          //negative direction
        {
            curZ++;
        }

        //X axis
        if (cursorHor > sensetivity && curX > 0)              //positive direction
        {
            curX--;
        }

        if (cursorHor < -sensetivity && curX < 9)           //negative durection
        {
            curX++;
        }

    }

    void moveFromLeft()
    {
        float cursorHor = 0;
        float cursorVert = 0;

        //Get new iput values
        cursorHor = Input.GetAxisRaw("Horizontal");
        cursorVert = Input.GetAxisRaw("Vertical");

        if (cursorVert > sensetivity && curX > 0)           //positive direction
        {
            curX--;
        }

        if (cursorVert < -sensetivity && curX < 9)          //negative direction
        {
            curX++;
        }

        //X axis
        if (cursorHor > sensetivity && curZ < 9)              //positive direction
        {
            curZ++;
        }

        if (cursorHor < -sensetivity && curZ > 0)           //negative durection
        {
            curZ--;
        }
    }

    void moveFromRight()
    {
        float cursorHor = 0;
        float cursorVert = 0;

        //Get new iput values
        cursorHor = Input.GetAxisRaw("Horizontal");
        cursorVert = Input.GetAxisRaw("Vertical");

        if (cursorVert > sensetivity && curX < 9)           //positive direction
        {
            curX++;
        }

        if (cursorVert < -sensetivity && curX > 0)          //negative direction
        {
            curX--;
        }

        //X axis
        if (cursorHor > sensetivity && curZ > 0)              //positive direction
        {
            curZ--;
        }

        if (cursorHor < -sensetivity && curZ < 9)           //negative durection
        {
            curZ++;
        }
    }

    void otherProjection()
    {
        string curPlane = planeDet.currentPlane;
        if (curTile != null && originalMat != null)
        {
            prevTile = curTile;
            Renderer[] prevRend = prevTile.GetComponentsInChildren<Renderer>();
            foreach (Renderer prevR in prevRend)
            {
                prevR.material.color = originalMat;
            }
        }

        //prevTile = curTile;
        curTile = gridScript.getTile(curPlane, curX, curZ);       //get new tile          

        //Valid tile
        if (curTile.GetComponent<TileData>().occupiedBy == null)
        {

            Renderer[] renderers = curTile.GetComponentsInChildren<Renderer>();
            originalMat = renderers[0].material.color;
            foreach (Renderer rend in renderers)
            {
                rend.material.color = Color.green;
            }
        }
        else    //not valid tile
        {
            GameObject occupant = curTile.GetComponent<TileData>().occupiedBy;
            if (occupant.tag == "UsedTile")
            {
                curTile = occupant;

                Renderer[] rendererss = occupant.GetComponentsInChildren<Renderer>();
                originalMat = rendererss[0].material.color;
                foreach (Renderer rendo in rendererss)
                {
                    rendo.material.color = Color.red;
                }
            }
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
        //selectionCurs.transform.position = initialSelCursPos;
        curX = 0;
        curZ = 0;
        // mainCamera.transform.position = initialCameraPos;
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
        if (triggerPressed != 0)                                          //If LT is pressed
        {
            triggerOn = true;
        }
        else
        {
            if (triggerOn)
                triggerOff = true;
        }

        if (triggerOn && triggerOff)
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
        // deleteProjection();
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
        //mainCamera.transform.position = initialCameraPos;
        //   deleteProjection();
    }

    //SELECTION CURSOR ACTIVE
    void onSelectCursorClick()
    {
        selMenu.MenuOn = true;

        if (curTile != null)
        {
            if (scoreSystem.canBuyTurret() && !selectingTurret)        //Building
            {
                if (curTile.tag != "UsedTile")
                {
                    if (curTile.GetComponent<TileData>().occupiedBy == null)
                    {
                        string currentPlane = planeDet.currentPlane;

                        GameObject newTurret = Instantiate(turret, curTile.transform.position, turret.transform.rotation) as GameObject;

                        alignToPlane(newTurret);

                        curTile.tag = "UsedTile";
                        curTile.GetComponent<TileData>().occupiedBy = newTurret;
                        cursorChange();

                        curTile = null;
                        scoreSystem.buyTurret();
                    }
                    else
                    {
                        cursorChange();
                    }
                }
                else                                                                                    //Attemptto build failed
                {
                    cursorChange();
                }

            }

            if (selectingTurret)                                                                        //Upgrading
            {
                Debug.Log("Upgrade?");
                currentTurret = curTile.GetComponent<TileData>().occupiedBy;
                if (currentTurret != null)
                {
                    Transform prevTransform = currentTurret.transform;
                    turretOffset upgrManager = currentTurret.GetComponent<turretOffset>();
                    GameObject newTurr = upgrManager.UpgradeTurret(prevTransform);

                    if (newTurr != null)
                    {
                        newTurr.transform.parent = currentTurret.transform.parent;
                        curTile.GetComponent<TileData>().occupiedBy = newTurr;
                        Destroy(currentTurret);
                        scoreSystem.upgradeTurret();

                        prevTile = null;
                        curTile = null;
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

    //-------------------------------MENU CURSOR ACTIVE---------------------------------
    void onMenuCursorClick()
    {
        // selMenuActive = false;

        if (buyTurretHit)
        {
            if (!menuActive)
            {
                menuActive = true;
                subMenu.SetActive(true);
                currentCurs = null;
                menuCurs.transform.position = initialMenCursPos;
            }
            selectingTurret = false;
            buyTurretHit = false;
            selMenu.currentButton = -10;

            selMenu.MenuOn = false;
        }

        //UPGRADE TURRET
        if (upgradeTurretHit)
        {
            selectingTurret = true;
            cursorChange();

            upgradeTurretHit = false;
            selMenu.currentButton = -10;
            selMenu.MenuOn = false;
        }

 // deleteProjection();
        //RaycastHit hit;
        //Vector3 rayOrigin = new Vector3();
        //rayOrigin = currentCurs.transform.position;
        //Ray cursorRay = new Ray(rayOrigin, Vector3.forward);  //Create a ray with cursor as an origin and down as a direction

 ////Some button was hit
        //if (Physics.Raycast(cursorRay, out hit))                     //If something was hit
        //{
        //    if (hit.collider.tag == "GUI")                         //Check if it is a tile
        //    {
        //        //BUY TURRET
        //        if (hit.collider.name == "buyTurret")
        //        {
        //            if (!menuActive)
        //            {
        //                menuActive = true;
        //                subMenu.SetActive(true);
        //                currentCurs = null;
        //                menuCurs.transform.position = initialMenCursPos;
        //            }
        //            selectingTurret = false;

 //        }

 //        //UPGRADE TURRET
        //        if (hit.collider.name == "upgrade")
        //        {
        //            Debug.Log("attempt to upgrade");
        //            selectingTurret = true;
        //            cursorChange();
        //        }

         //WAVE SPAWN
        //if (hit.collider.name == "waveSpawn")
        //{
        //    waveManager.spawnNewWave = true;

         //    //Sound
        //    audioMangr = GameObject.FindGameObjectWithTag("Audio");
        //    if (audioMangr.activeSelf)
        //    {
        //        inGameAudio gameAudio = audioMangr.GetComponent<inGameAudio>();
        //        gameAudio.waveSource.Play();
        //    }
        //}
        //}
        //}
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

    void alignToPlane(GameObject newTurret)
    {
        string currentPlane = planeDet.currentPlane;

        turretOffset offset = newTurret.GetComponent<turretOffset>();
        float sizeY = newTurret.GetComponent<BoxCollider>().bounds.size.y;
        Vector3 newPos;

        switch (currentPlane)
        {
            case "TopPlane":
                newTurret.transform.parent = GameObject.Find("TopSide").transform;

                newPos = curTile.transform.localPosition;
                newPos.y += sizeY;

                //Add the offset
                newPos.x += offset.xOffset;
                newPos.z -= offset.zOffset;

                newTurret.transform.localPosition = newPos;
                newTurret.transform.localRotation = curTile.transform.localRotation;
                break;
            case "FarPlane":
                newTurret.transform.parent = GameObject.Find("FarSide").transform;

                newPos = curTile.transform.localPosition;

                sizeY = newTurret.GetComponent<BoxCollider>().bounds.size.y;
                newPos.y += sizeY;

                //Add the offset
                newPos.x += offset.xOffset;
                newPos.z -= offset.zOffset;

                newTurret.transform.localPosition = newPos;
                newTurret.transform.localRotation = curTile.transform.localRotation;

                break;
            case "NearPlane":
                newTurret.transform.parent = GameObject.Find("NearSide").transform;

                newPos = curTile.transform.localPosition;

                sizeY = newTurret.GetComponent<BoxCollider>().bounds.size.y;
                newPos.y += sizeY;

                //Add the offset
                newPos.x += offset.xOffset;
                newPos.z -= offset.zOffset;

                newTurret.transform.localPosition = newPos;
                newTurret.transform.localRotation = curTile.transform.localRotation;

                break;
            case "BotPlane":
                newTurret.transform.parent = GameObject.Find("BotSide").transform;

                newPos = curTile.transform.localPosition;

                sizeY = newTurret.GetComponent<BoxCollider>().bounds.size.y;
                newPos.y += sizeY;

                //Add the offset
                newPos.x += offset.xOffset;
                newPos.z -= offset.zOffset;

                newTurret.transform.localPosition = newPos;
                newTurret.transform.localRotation = curTile.transform.localRotation;

                break;
            case "LeftPlane":
                newTurret.transform.parent = GameObject.Find("LeftSide").transform;

                newPos = curTile.transform.localPosition;

                sizeY = newTurret.GetComponent<BoxCollider>().bounds.size.y;
                newPos.y += sizeY;

                //Add the offset
                newPos.x += offset.xOffset;
                newPos.z -= offset.zOffset;

                newTurret.transform.localPosition = newPos;
                newTurret.transform.localRotation = curTile.transform.localRotation;

                break;
            case "RightPlane":
                newTurret.transform.parent = GameObject.Find("RightSide").transform;

                newPos = curTile.transform.localPosition;

                sizeY = newTurret.GetComponent<BoxCollider>().bounds.size.y;
                newPos.y += sizeY;

                //Add the offset
                newPos.x += offset.xOffset;
                newPos.z -= offset.zOffset;

                newTurret.transform.localPosition = newPos;
                newTurret.transform.localRotation = curTile.transform.localRotation;

                break;

        }
    }


}