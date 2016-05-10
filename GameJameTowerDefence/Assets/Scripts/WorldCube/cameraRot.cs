using UnityEngine;
using System.Collections;

public class cameraRot : MonoBehaviour {

    // Use this for initialization
    private int currAngle;
    private int maxAngleZ;
    private int maxAngleX;
    private bool posDirectionZ;
    private bool posDirectionX;
    public bool rotateCubeZ = false;
    public bool rotateCubeX = false;
    private Vector3 direction;

    private float rotationX;
    private float rotationZ;

    float sensetivity = 0.7f;
    //Timer
    public float curTime = 0;
    public float timeInterval = 0.1f;

    Quaternion targetRotation;
    void Start()
    {
        currAngle = 0;
        maxAngleZ = 0;
        maxAngleX = 0;
        posDirectionZ = false;
    }

    // Update is called once per frame
    void Update()
    {
        setRotation();

        if (rotateCubeZ)
            doRotationZ(targetRotation);

        if (rotateCubeX)
            doRotationX(targetRotation);
    }

    void setRotation()
    {
        rotationX = Input.GetAxis("RotationVertical");
        rotationZ = Input.GetAxis("RotationHorizontal");

        //--------------------ROTATION---------------------\\

        if ((Input.GetKeyDown(KeyCode.UpArrow) && rotateCubeZ == false && rotateCubeX == false) || (rotationZ < -sensetivity && rotateCubeZ == false && rotateCubeX == false))
        {
            maxAngleZ += 90;
            targetRotation = Quaternion.Euler(maxAngleX, 0, maxAngleZ);
            rotateCubeZ = true;
        }

        if ((Input.GetKeyDown(KeyCode.DownArrow) && rotateCubeZ == false) || (rotationZ > sensetivity && rotateCubeZ == false && rotateCubeX == false))
        {
            maxAngleZ -= 90;
            targetRotation = Quaternion.Euler(maxAngleX, 0, maxAngleZ);
            rotateCubeZ = true;
        }

        if ((Input.GetKeyDown(KeyCode.LeftArrow) && rotateCubeX == false) || (rotationX < -sensetivity && rotateCubeZ == false && rotateCubeX == false))
        {
            maxAngleX -= 90;
            targetRotation = Quaternion.Euler(maxAngleX, 0, maxAngleZ);
            rotateCubeX = true;
        }

        if ((Input.GetKeyDown(KeyCode.RightArrow) && rotateCubeX == false) || (rotationX > sensetivity && rotateCubeZ == false && rotateCubeX == false))
        {
            maxAngleX += 90;
            targetRotation = Quaternion.Euler(maxAngleX, 0, maxAngleZ);
            rotateCubeX = true;
        }

    }

    //-------------------ROTATION Z----------------------
    void doRotationZ(Quaternion target)
    {

        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 10);
        if (this.transform.rotation == targetRotation)
        {
            rotateCubeZ = false;
        }

    }
    //-------------------ROTATION Z----------------------

    //___________________ROTATION X____________________

    void doRotationX(Quaternion target)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 10);
        if (this.transform.rotation == targetRotation)
        {
            rotateCubeX = false;
        }
    }
}
