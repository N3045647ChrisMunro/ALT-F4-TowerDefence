using UnityEngine;
using System.Collections;

public class turretOffset : MonoBehaviour {

    public float xOffset = 0;
    public float zOffset = 0;
    public string type;
    public int upgradeNumber;

    public GameObject nextTurret;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public GameObject UpgradeTurret(Transform prevTurret)
    {
        if(nextTurret!=null)
        {
            turretOffset nextOffset = nextTurret.GetComponent<turretOffset>();
            Vector3 turretPos = prevTurret.transform.position;
            turretPos.x -= nextOffset.xOffset;
            turretPos.z -= nextOffset.zOffset;
            GameObject newTurr = Instantiate(nextTurret, turretPos, prevTurret.transform.rotation) as GameObject;
            newTurr.transform.position = turretPos;
            return newTurr;
        }
        return null;
    }

}
