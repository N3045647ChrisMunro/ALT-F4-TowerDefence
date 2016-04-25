using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;

public class ReadFile : MonoBehaviour
{

    private XmlDocument xmlFile;

    //public Waypoints waypoints;
    public int[] mapData;
    public int[] mapDataRight;

    // Use this for initialization
    void Start()
    {

        mapData = new int[200];
        mapDataRight = new int[200];

        try
        {
            System.IO.StreamReader reader = new System.IO.StreamReader("Assets/Scripts/WorldCube/Pathdata/pathdata.txt");

            string line;

            line = reader.ReadLine();

            if (line != "Pathdata")
            {
                Debug.Log("Wrong File Loaded");
                return;
            }
            using (reader)
            {
                readMap(reader, "Top", mapData);
                readMap(reader, "Right", mapDataRight);
            }
            Debug.Log(mapDataRight[0] + ", " + mapDataRight[1]);

            reader.Close();

        }
        catch (System.Exception e)
        {
            Debug.Log("Exception: " + e);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    void readMap(System.IO.StreamReader reader, string cubeSide, int[] dataArray)
    {
        //dataArray = new int[200];
        int arrayTracker = 0;

        string line;

        line = reader.ReadLine();

        if (line != cubeSide)
        {
            Debug.Log("Cannot find CubeSide Data in File");
        }

        for (uint j = 0; j < 10; j++)
        {
            line = reader.ReadLine();

            if (line != null)
            {

                string[] entries = line.Split(',');

                for (uint i = 0; i < entries.Length; i++)
                {
                    if (entries[i] == "0")
                    {
                        dataArray[arrayTracker] = -1;
                        arrayTracker++;
                    }
                    if (entries[i] == "@")
                    {
                        dataArray[arrayTracker] = 0;
                        arrayTracker++;
                    }
                    if (entries[i] != "@" && entries[i] != "0")
                    {
                        dataArray[arrayTracker] = int.Parse(entries[i]);
                        arrayTracker++;
                    }
                }

            }
        }

        line = reader.ReadLine();
        if (line == "END")
        {
            Debug.Log("GOT TO THE END");
            return;
        }
    }



}
