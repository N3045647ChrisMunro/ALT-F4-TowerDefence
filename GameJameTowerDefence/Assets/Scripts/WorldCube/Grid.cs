using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour
{

    public GameObject cube;
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

        Vector3 size = cube.GetComponent<Renderer>().bounds.size;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Vector3 pos = new Vector3(posX, 0, posZ);
                GameObject newTile = Instantiate(cube, pos, cube.transform.rotation) as GameObject;
                newTile.transform.SetParent(this.transform, false);

                cubeTileArray[x, y] = newTile;

                posX += size.x;

            }

            posZ += size.z;
            posX = 0;

        }

    }
}