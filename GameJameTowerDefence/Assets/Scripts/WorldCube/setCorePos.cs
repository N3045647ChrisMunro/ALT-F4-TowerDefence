using UnityEngine;
using System.Collections;

public class setCorePos : MonoBehaviour {

    public GameObject Core;
	// Use this for initialization
	void Start () {
        GameObject endCore = Instantiate(Core, new Vector3(0f, 0f, 0f), this.transform.rotation) as GameObject;
        endCore.transform.parent = this.transform;
        endCore.transform.localPosition = new Vector3(3.04f, 0.46f, 17.15f);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
