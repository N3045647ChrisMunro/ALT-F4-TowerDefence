using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour
{

    //public GameObject cube1;
    public GameObject[] cubes;
    public GameObject parent;

    private int width = 10;
    private int height = 10;

    public GameObject[,] cubeSideTileArray_TOP;
    public GameObject[,] cubeSideTileArray_RIGHT;
    public GameObject[,] cubeSideTileArray_LEFT;
    public GameObject[,] cubeSideTileArray_NEAR;
    public GameObject[,] cubeSideTileArray_FAR;
    public GameObject[,] cubeSideTileArray_BOT;

    public GameObject waypointsParent;
    public List<GameObject> emptyGameObjs;
    public List<Transform> waypoints;
    private int waypointTracker;

    // public CubeSide[] worldCube;
    private const int numSides_ = 6;

    float posX = 0;
    float posZ = 0;

    void Awake()
    {
        waypointTracker = 0;
        createCube();
    }

    void createCube()
    {
        cubeSideTileArray_TOP = new GameObject[width, height];
        cubeSideTileArray_RIGHT = new GameObject[width, height];
        cubeSideTileArray_LEFT = new GameObject[width, height];
        cubeSideTileArray_NEAR = new GameObject[width, height];
        cubeSideTileArray_FAR = new GameObject[width, height];
        cubeSideTileArray_BOT = new GameObject[width, height];

        createSide("Top", cubeSideTileArray_TOP, 0, 1);
        createSide("Right", cubeSideTileArray_RIGHT, 2, 3);
        createSide("Left", cubeSideTileArray_LEFT, 4, 5);
        createSide("Near", cubeSideTileArray_NEAR, 6, 7);
        createSide("Far", cubeSideTileArray_FAR, 8, 9);
        createSide("Bot", cubeSideTileArray_BOT, 10, 11);

        readWaypointData();

    }

    void createSide(string side, GameObject[,] cubeSideTileArray, int indexStart, int indexFinish)
    {
        try
        {

            System.IO.StreamReader reader = new System.IO.StreamReader("Assets/Scripts/WorldCube/Pathdata/WorldDataFile.txt");

            string line;

            line = reader.ReadLine();

            if (line != "WorldDataFile")
            {
                Debug.Log("Wrong File Loaded");
                return;
            }
            else
            {

                using (reader)
                {

                    //cubeSideTileArray = new GameObject[width, height];

                    Vector3 size = cubes[0].GetComponentInChildren<Renderer>().bounds.size;

                    line = reader.ReadLine();

                    while (line != side)
                    {
                        line = reader.ReadLine();
                    }

                    if (line == side)
                    {
                        parent = GameObject.FindGameObjectWithTag(side);
                    }


                    for (int y = 0; y < height; y++)
                    {

                        line = reader.ReadLine();

                        string[] entries = line.Split(',');

                        for (int x = 0; x < width; x++)
                        {
                            if (entries[x] == "0")
                            {
                                Vector3 pos = new Vector3(posX, 0, posZ);
                                int index = Random.Range(indexStart, indexFinish + 1);
                                GameObject newTile = Instantiate(cubes[index], transform.position, cubes[index].transform.rotation) as GameObject;
                                newTile.transform.position = pos;
                                newTile.transform.SetParent(parent.transform, false);

                                newTile.GetComponent<TileData>().idx_X = x;
                                newTile.GetComponent<TileData>().idx_Z = y;

                                cubeSideTileArray[x, y] = newTile;


                            }
                            if (entries[x] == "@")
                            {
                                Vector3 pos = new Vector3(posX, 0, posZ);

                                GameObject newTile = Instantiate(cubes[0], pos, cubes[0].transform.rotation) as GameObject;
                                newTile.transform.SetParent(parent.transform, false);

                                newTile.GetComponent<TileData>().idx_X = x;
                                newTile.GetComponent<TileData>().idx_Z = y;

                                //This adds a tile ontop of the plane tile
                                pos.y = 0.049f;

                                GameObject pathTile = Instantiate(cubes[13], pos, cubes[13].transform.rotation) as GameObject;
                                pathTile.transform.SetParent(parent.transform, false);

                                pathTile.GetComponent<TileData>().idx_X = x;
                                pathTile.GetComponent<TileData>().idx_Z = y;
                                newTile.GetComponent<TileData>().occupiedBy = pathTile;

                                cubeSideTileArray[x, y] = newTile;
                            }
                            posX += size.x * 2;
                        }

                        posZ += size.z;
                        posX = 0;

                    }

                    reader.Close();

                }


            }
        }
        catch (System.Exception e)
        {
            Debug.Log("Exception: " + e);
        }
    }

    void readWaypointData()
    {
        try
        {

            System.IO.StreamReader reader = new System.IO.StreamReader("Assets/Scripts/WorldCube/Pathdata/WorldDataFile.txt");

            string line;

            line = reader.ReadLine();

            if (line != "WorldDataFile")
            {
                Debug.Log("Wrong File Loaded");
                return;
            }
            else
            {
                using (reader)
                {
                    line = reader.ReadLine();

                    while (line != "Waypoints")
                    {
                        line = reader.ReadLine();
                    }

                    if (line == "Waypoints")
                    {

                        line = reader.ReadLine();

                        //READING TOP Plane Waypoint information
                        if (line == "Top")
                        {


                        }
                        else
                        {
                            Debug.Log("Something Went Wrong");
                            return;
                        }

                        line = reader.ReadLine();

                        string[] idxs = line.Split(':');

                        for (int i = 0; i < idxs.Length; i++)
                        {
                            string[] tileIDX = idxs[i].Split(',');

                            emptyGameObjs.Add(new GameObject());
                            emptyGameObjs[waypointTracker].transform.SetParent(GameObject.FindGameObjectWithTag("Top").transform, false);

                            int x = int.Parse(tileIDX[0]);
                            int z = int.Parse(tileIDX[1]);

                            emptyGameObjs[waypointTracker].transform.localPosition = getTilePosition("Top", x, z);

                            waypoints.Add(emptyGameObjs[waypointTracker].transform);

                            waypointTracker++;

                        }

                        //Reading Far plane Waypoint Information
                        line = reader.ReadLine();

                        if (line == "Far")
                        {


                        }
                        else
                        {
                            Debug.Log("Something Went Wrong");
                            return;
                        }

                        line = reader.ReadLine();

                        idxs = line.Split(':');

                        for (int i = 0; i < idxs.Length; i++)
                        {
                            string[] tileIDX = idxs[i].Split(',');

                            emptyGameObjs.Add(new GameObject());

                            int x = int.Parse(tileIDX[0]);
                            int z = int.Parse(tileIDX[1]);

                            emptyGameObjs[waypointTracker].transform.localPosition = getTilePosition("Far", x, z);

                            Quaternion worldRot = this.transform.localRotation;

                            emptyGameObjs[waypointTracker].transform.rotation = worldRot;

                            emptyGameObjs[waypointTracker].transform.SetParent(GameObject.FindGameObjectWithTag("Far").transform, false);
                            waypoints.Add(emptyGameObjs[waypointTracker].transform);

                            waypointTracker++;

                        }
                        //Reading Left plane Waypoint Information
                        line = reader.ReadLine();

                        if (line == "Left")
                        {


                        }
                        else
                        {
                            Debug.Log("Something Went Wrong");
                            return;
                        }

                        line = reader.ReadLine();

                        idxs = line.Split(':');

                        for (int i = 0; i < idxs.Length; i++)
                        {
                            string[] tileIDX = idxs[i].Split(',');

                            emptyGameObjs.Add(new GameObject());

                            int x = int.Parse(tileIDX[0]);
                            int z = int.Parse(tileIDX[1]);

                            emptyGameObjs[waypointTracker].transform.localPosition = getTilePosition("Left", x, z);

                            Quaternion worldRot = this.transform.localRotation;

                            emptyGameObjs[waypointTracker].transform.rotation = worldRot;

                            emptyGameObjs[waypointTracker].transform.SetParent(GameObject.FindGameObjectWithTag("Left").transform, false);
                            waypoints.Add(emptyGameObjs[waypointTracker].transform);

                            waypointTracker++;
                        }

                        //Reading Bot plane Waypoint Information
                        line = reader.ReadLine();

                        if (line == "Bot")
                        {


                        }
                        else
                        {
                            Debug.Log("Something Went Wrong");
                            return;
                        }

                        line = reader.ReadLine();

                        idxs = line.Split(':');

                        for (int i = 0; i < idxs.Length; i++)
                        {
                            string[] tileIDX = idxs[i].Split(',');

                            emptyGameObjs.Add(new GameObject());

                            int x = int.Parse(tileIDX[0]);
                            int z = int.Parse(tileIDX[1]);

                            emptyGameObjs[waypointTracker].transform.localPosition = getTilePosition("Bot", x, z);

                            Quaternion worldRot = this.transform.localRotation;

                            emptyGameObjs[waypointTracker].transform.rotation = worldRot;

                            emptyGameObjs[waypointTracker].transform.SetParent(GameObject.FindGameObjectWithTag("Bot").transform, false);
                            waypoints.Add(emptyGameObjs[waypointTracker].transform);

                            waypointTracker++;

                        }

                        //Reading Right plane Waypoint Information
                        line = reader.ReadLine();

                        if (line == "Right")
                        {

                        }
                        else
                        {
                            Debug.Log("Something Went Wrong");
                            return;
                        }

                        line = reader.ReadLine();

                        idxs = line.Split(':');

                        for (int i = 0; i < idxs.Length; i++)
                        {
                            string[] tileIDX = idxs[i].Split(',');

                            emptyGameObjs.Add(new GameObject());

                            int x = int.Parse(tileIDX[0]);
                            int z = int.Parse(tileIDX[1]);

                            emptyGameObjs[waypointTracker].transform.localPosition = getTilePosition("Right", x, z);

                            Quaternion worldRot = this.transform.localRotation;

                            emptyGameObjs[waypointTracker].transform.rotation = worldRot;

                            emptyGameObjs[waypointTracker].transform.SetParent(GameObject.FindGameObjectWithTag("Right").transform, false);
                            waypoints.Add(emptyGameObjs[waypointTracker].transform);

                            waypointTracker++;

                        }

                        //Reading Near plane Waypoint Information
                        line = reader.ReadLine();

                        if (line == "Near")
                        {


                        }
                        else
                        {
                            Debug.Log("Something Went Wrong");
                            return;
                        }

                        line = reader.ReadLine();

                        idxs = line.Split(':');

                        for (int i = 0; i < idxs.Length; i++)
                        {
                            string[] tileIDX = idxs[i].Split(',');

                            emptyGameObjs.Add(new GameObject());

                            int x = int.Parse(tileIDX[0]);
                            int z = int.Parse(tileIDX[1]);

                            emptyGameObjs[waypointTracker].transform.localPosition = getTilePosition("Near", x, z);

                            Quaternion worldRot = this.transform.localRotation;

                            emptyGameObjs[waypointTracker].transform.rotation = worldRot;

                            emptyGameObjs[waypointTracker].transform.SetParent(GameObject.FindGameObjectWithTag("Near").transform, false);
                            waypoints.Add(emptyGameObjs[waypointTracker].transform);

                            waypointTracker++;

                        }


                    }
                    reader.Close();
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.Log("Exception in Read Waypoint Data" + e);
        }
    }

    public GameObject getTile(string side, int x_IDX, int z_IDX)
    {

        //GameObject temp = new GameObject();
        //temp.name = "lalalalalalal";

        switch (side)
        {
            case "TopPlane":
                GameObject  temp = cubeSideTileArray_TOP[x_IDX, z_IDX];
                return temp;
                break;

            case "RightPlane":
                GameObject  temp1 = cubeSideTileArray_RIGHT[x_IDX, z_IDX];
                return temp1;
                break;

            case "LeftPlane":
                GameObject temp2 = cubeSideTileArray_LEFT[x_IDX, z_IDX];
                return temp2;
                break;

            case "FarPlane":
                GameObject temp3 = cubeSideTileArray_FAR[x_IDX, z_IDX];
                return temp3;
                break;

            case "NearPlane":
                GameObject temp4 = cubeSideTileArray_NEAR[x_IDX, z_IDX];
                return temp4;
                break;

            case "BotPlane":
                GameObject temp5 = cubeSideTileArray_BOT[x_IDX, z_IDX];
                return temp5;
                break;
        }

        return null;
    }

    public Vector3 getTilePosition(string side, int x_IDX, int z_IDX)
    {
        //Size of the tile, so the half length can be substracted from the
        //position to centre it in a 4x2 tile
        Vector3 tempPos = new Vector3();

        switch (side)
        {
            case "Top":
                tempPos = cubeSideTileArray_TOP[x_IDX, z_IDX].transform.localPosition;
                break;
            case "Right":
                tempPos = cubeSideTileArray_RIGHT[x_IDX, z_IDX].transform.localPosition;
                break;
            case "Left":
                tempPos = cubeSideTileArray_LEFT[x_IDX, z_IDX].transform.localPosition;
                break;
            case "Far":
                tempPos = cubeSideTileArray_FAR[x_IDX, z_IDX].transform.localPosition;
                break;
            case "Near":
                tempPos = cubeSideTileArray_NEAR[x_IDX, z_IDX].transform.localPosition;
                break;
            case "Bot":
                tempPos = cubeSideTileArray_BOT[x_IDX, z_IDX].transform.localPosition;
                break;
        }

        //Create the new position with a custom Y that's above the plane, this is helpful
        //for when setting waypoints/spawn points (stops enemies going half into the plane);
        Vector3 newPos = new Vector3(tempPos.x, 0.39f, tempPos.z);

        return newPos;
    }

    public void ChangeTileColour(Color colour, int x_IDX, int z_IDX)
    {
        Renderer renderer = cubeSideTileArray_TOP[x_IDX, z_IDX].GetComponent<Renderer>();

        renderer.material.color = colour;
    }

}