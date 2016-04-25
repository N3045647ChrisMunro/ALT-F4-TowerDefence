using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour
{

    //public GameObject cube1;
    public GameObject[] cubes;
    public GameObject parent;
    public GameObject side;

    private int width = 20;
    private int height = 10;

    public GameObject[,] cubeTileArray;

    // public CubeSide[] worldCube;
    private const int numSides_ = 6;

    public Grid worldGrid;

    float posX = 0;
    float posZ = 0;

    void Awake()
    {
        createCube();
    }

    void createCube()
    {
        cubeTileArray = new GameObject[width, height];

        Vector3 size = cubes[0].GetComponent<Renderer>().bounds.size;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {

                //Pick random colour cube to place
                int randIndex = Random.Range(0, cubes.Length);

                GameObject newRandTile = cubes[randIndex];
                
                Vector3 pos = new Vector3(posX, 0, posZ);
                GameObject newTile = Instantiate(newRandTile, pos, newRandTile.transform.rotation) as GameObject;
                newTile.transform.SetParent(this.transform, false);

                cubeTileArray[x, y] = newTile;

                posX += size.x;

            }

            posZ += size.z;
            posX = 0;

        }

    }

    public Vector3 getTilePosition(int x_IDX, int z_IDX)
    {
        //Size of the tile, so the half length can be substracted from the
        //position to centre it in a 4x2 tile
        Vector3 tileSize = cubes[0].GetComponent<Renderer>().bounds.size;

        Vector3 tempPos = cubeTileArray[x_IDX, z_IDX].transform.localPosition;

        //Create the new position with a custom Y that's above the plane, this is helpful
        //for when setting waypoints/spawn points (stops enemies going half into the plane);
        Vector3 newPos = new Vector3(tempPos.x + tileSize.x / 2, 0.36f, tempPos.z - (tileSize.z / 2));

        return newPos;
    }

    public void ChangeTileColour(Color colour, int x_IDX, int z_IDX)
    {
        Renderer renderer = cubeTileArray[x_IDX, z_IDX].GetComponent<Renderer>();

        renderer.material.color = colour;
    }

}