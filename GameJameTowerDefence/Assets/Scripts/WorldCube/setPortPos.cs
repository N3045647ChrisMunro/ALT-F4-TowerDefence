using UnityEngine;
using System.Collections;

public class setPortPos : MonoBehaviour {

    public GameObject portal;
	// Use this for initialization
	void Start () {

        GameObject startPortal = Instantiate(portal, new Vector3(0f, 0f, 0f), this.transform.rotation) as GameObject;
        startPortal.transform.parent = this.transform;
        startPortal.transform.localPosition = new Vector3(4.3f, 0f, 3.92f);
        startPortal.transform.localRotation = Quaternion.Euler(270f, 90f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
