using UnityEngine;
using System.Collections;

public class indexDetect : MonoBehaviour {

	// Use this for initialization
    public int currentXIndex= -5;
    public int currentZIndex = -5;

    public planeDetector planedet;

	void Start () {
	
	}
	
    /* Chris show great respect and surprise about magical, fantastic, wonderful guessing abilities of Kristina, the goodess of planes*/

	// Update is called once per frame
	void Update () 
    {
       if (planedet.currentPlane=="TopPlane")
           getindex(Vector3.down);

       if (planedet.currentPlane == "BotPlane")
           getindex(Vector3.up);

       if (planedet.currentPlane == "RightPlane")
           getindex(Vector3.back);

       if (planedet.currentPlane == "LeftPlane")            
           getindex(Vector3.forward);

       if (planedet.currentPlane == "NearPlane")
           getindex(Vector3.left);

       if (planedet.currentPlane == "FarPlane")
           getindex(Vector3.right);

	}

    void getindex(Vector3 dir)
    {
        RaycastHit hit;
        Vector3 rayOrigin = new Vector3();
        rayOrigin = this.transform.position;

        Ray cursorRay = new Ray(rayOrigin, dir);  //Create a ray with cursor as an origin and down as a direction
        Debug.DrawRay(rayOrigin, dir, Color.blue, 10);

        if (Physics.Raycast(cursorRay, out hit))                     //If something was hit
        {
            GameObject babyTile = hit.collider.gameObject;
            GameObject parentTile = babyTile.transform.parent.gameObject;
            currentXIndex = parentTile.GetComponent<TileData>().idx_X;
            currentZIndex = parentTile.GetComponent<TileData>().idx_Z;
        }
    }
}
