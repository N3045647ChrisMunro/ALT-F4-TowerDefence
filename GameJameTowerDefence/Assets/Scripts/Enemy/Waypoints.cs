using UnityEngine;
using System.Collections;

public class Waypoints : MonoBehaviour {

    public Transform[] waypoints;
    private GameObject[] emptyGameObject;


    public ReadFile fileData;
    public Grid topGrid;
    public Grid rightGrid;

    private const int numWayPoints = 14;
    int tracker;

    void Start()
    {
        waypoints = new Transform[numWayPoints];
        emptyGameObject = new GameObject[numWayPoints];

        for (uint i = 0; i < numWayPoints; i++)
        {
            emptyGameObject[i] = new GameObject();

            //Instantiate(emptyGameObject[i]);
            waypoints[i] = emptyGameObject[i].transform;
            waypoints[i].transform.parent = this.gameObject.transform;
        }

        tracker = 1;

    }

    void Update()
    {
        setWayPoints();
    }

    void setSideWayPoints(int[] mapData, Grid gridSide, int arrayIdx)
    {
        tracker = arrayIdx;
        //TODO COMMENT THIS
        if (mapData.Length > 0)
        {
            for (int z = 0; z < 10; z++)
            {
                for (int x = 0; x < 20; x++)
                {
                    int idx = x + z * 20;

                    if (mapData[idx] != -1 && mapData[idx] != 0)
                    {
                        if (mapData[idx] == tracker)
                        {
                            Vector3 pos = gridSide.getTilePosition(x, z);

                            emptyGameObject[arrayIdx - 1].transform.position = pos;

                            if (tracker >= numWayPoints)
                            {
                                tracker = numWayPoints;
                            }
                            else
                            {
                                tracker++;
                            }

                        }

                        gridSide.ChangeTileColour(Color.black, x, z);

                    }
                    if (mapData[idx] == 0)
                    {
                        gridSide.ChangeTileColour(Color.black, x, z);
                    }
                }
            }

        }
        else
        {
            Debug.Log("No data");
        }
    }

    void setWayPoints()
    {
        Debug.Log("tracker: " + tracker);
        setSideWayPoints(fileData.mapData, topGrid, 1);
        Debug.Log("tracker: " + tracker);
        setSideWayPoints(fileData.mapDataRight, rightGrid, 8);
       // Debug.Log("tracker: " + tracker);
    }

}
