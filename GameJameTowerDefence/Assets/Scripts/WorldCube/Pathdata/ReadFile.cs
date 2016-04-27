using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;

public class ReadFile : MonoBehaviour {

    private XmlDocument xmlFile;

    //public Waypoints waypoints;
    public int[] mapData;

	// Use this for initialization
	void Start () {

        mapData = new int[200];
        int arrayTracker = 0;

        try
        {
            System.IO.StreamReader reader = new System.IO.StreamReader("Assets/Scripts/WorldCube/PathdataXML/pathdata.txt");

            string line;

            line = reader.ReadLine();

            if (line != "Pathdata")
            {
                Debug.Log("Wrong File Loaded");
                return;
            }

            line = reader.ReadLine();

            using (reader)
            {
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
                                mapData[arrayTracker] = -1;
                                arrayTracker++;
                            }
                            if (entries[i] == "@")
                            {
                                mapData[arrayTracker] = 0;
                                arrayTracker++;
                            }
                            if (entries[i] != "@" && entries[i] != "0")
                            {
                                mapData[arrayTracker] = int.Parse(entries[i]);
                                arrayTracker++;
                            }
                        }

                    }

                }
                //while (line != null);

                reader.Close();
            }

        }
        catch (System.Exception e)
        {
            Debug.Log("Exception: " + e);
        }

        //System.Array.Reverse(mapData);
        /*
        for (int i = 0; i < mapData.Length; i++)
        {
            Debug.Log(mapData[i]);
        }
        */
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
