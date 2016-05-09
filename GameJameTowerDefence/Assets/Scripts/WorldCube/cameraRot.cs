using UnityEngine;
using System.Collections;

public class cameraRot : MonoBehaviour
{

    // Use this for initialization
    private int currAngle;
    private int maxAngleZ;
    private int maxAngleX;
    private bool posDirectionZ;
    private bool posDirectionX;
    public bool rotateCubeZ = false;
    public bool rotateCubeX = false;
    private Vector3 direction;
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

        if (Input.GetKeyDown(KeyCode.UpArrow) && rotateCubeZ == false)
        {
            direction = Vector3.forward;
            maxAngleZ = (int)this.transform.rotation.eulerAngles.z + 90;
            posDirectionZ = true;
            rotateCubeZ = true;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && rotateCubeZ == false)
        {
            direction = Vector3.back;
            maxAngleZ = (int)this.transform.rotation.eulerAngles.z + 270;
            posDirectionZ = false;
            rotateCubeZ = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && rotateCubeX == false)
        {
            direction = Vector3.right;
            maxAngleX = (int)this.transform.rotation.eulerAngles.x + 90;
            posDirectionX = true;
            rotateCubeX = true;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = Vector3.right;
            rotateCubeX = true;
        }

        Debug.Log("mxA " + maxAngleX);

        if (rotateCubeZ)
            doRotationZ(direction, posDirectionZ);

        if (rotateCubeX)
            doRotationX(direction, posDirectionX);

    }
    void doRotationZ(Vector3 dir, bool positiveDir)
    {
        if (positiveDir == true)
        {
            if (maxAngleZ > 271)
            {
                transform.Rotate(dir, currAngle + 1);
                if (this.transform.rotation.eulerAngles.z <= 1)
                {
                    rotateCubeZ = false;
                }
            }
            else
            {
                transform.Rotate(dir, currAngle + 1);
                if (this.transform.rotation.eulerAngles.z >= maxAngleZ)
                {
                    rotateCubeZ = false;
                }
            }
        }
        else
        {
            Debug.Log(dir);
            transform.Rotate(dir, currAngle + 1);
            if (this.transform.rotation.eulerAngles.z == 270)
            {
                rotateCubeZ = false;
            }
        }

    }

    void doRotationX(Vector3 dir, bool positiveDir)
    {
        if (positiveDir == true)
        {
            Debug.Log("Hi");
            transform.Rotate(dir, currAngle + 1);
            if (this.transform.rotation.eulerAngles.x >= maxAngleX)
            {
                rotateCubeX = false;
            }
        }


    }

}
