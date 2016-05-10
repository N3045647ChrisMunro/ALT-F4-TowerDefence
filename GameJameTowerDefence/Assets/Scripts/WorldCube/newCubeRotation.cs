using UnityEngine;
using System.Collections;

public class newCubeRotation : MonoBehaviour {

    public GameObject[] cubesX;
    int curX = 0;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (curX < cubesX.Length - 1)
            {
                curX++;
            }
            else
            {
                curX = 0;
            }

            ApplyRo();
        }
	}

    void ApplyRo()
    {
        this.transform.rotation = cubesX[curX].transform.rotation;
    }
}
