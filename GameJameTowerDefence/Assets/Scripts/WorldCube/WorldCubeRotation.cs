using UnityEngine;
using System.Collections;

public class WorldCubeRotation : MonoBehaviour {

    public float sensetivity = 0.7f;
    
    
    private float currentAngleX = 0;
    private float currentAngleZ = 0;
    private float rotationX = 0;
    private float rotationZ = 0;

    private float delayTime = 0.4f;
    private float currentTime = 0.4f;

	// Use this for initialization
	void Start () 
    {
        currentTime = delayTime;
	}
	
	// Update is called once per frame
	void Update () 
    {
        rotateCube();

        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            setCubeRotation();
        }

	}

    void rotateCube()
    {
        //ROTATION IN X AXIS
        rotationX = Input.GetAxis("RotationVertical");
        rotationZ = Input.GetAxis("RotationHorizontal");
    }


    void setCubeRotation()
    {
        //Positive direction
        if (rotationX > sensetivity)
        {
            currentAngleX += 90;
            if (currentAngleX > 300)
            {
                currentAngleX = 0;
            }
        }

        //Negative direction
        if (rotationX < -sensetivity)
        {
            currentAngleX -= 90;
            if (currentAngleX < -400)
            {
                currentAngleX = 0;
            }
        }

        //Positive Rotation
        if (rotationZ > sensetivity)
        {
            currentAngleZ += 90;
            if (currentAngleZ > 300)
            {
                currentAngleZ = 0;
            }
        }

        //Negative Rotation
        if (rotationZ < -sensetivity)
        {
            currentAngleZ -= 90;
            if (currentAngleZ < -400)
            {
                currentAngleZ = 0;
            }
        }

        Quaternion angle = Quaternion.Euler(currentAngleX, 0, currentAngleZ);
        transform.rotation = angle;

        currentTime = delayTime;
    }
}
