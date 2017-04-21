using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camFollowMouse : MonoBehaviour
{

    public GameObject cam;

    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseY;
    public float sensitivityX = 8F;
    public float sensitivityY = 8F;
    public float minimumX = 90F;
    public float maximumX = 360F;
    public float minimumY = 90F;
    public float maximumY = 360F;
    float rotationY = 0F;
    float rotationX = 0F;

    void Update()
    {

        
           

            rotationX += Input.GetAxis("Mouse X") * sensitivityX;
            rotationX = Mathf.Clamp(rotationX, -20, 20);

            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, 0, 90);


        if (Input.GetKey("space"))
        {
            cam.transform.localEulerAngles = new Vector3(90 - rotationY, rotationX, 0);
        }
        else
        {
            cam.transform.localEulerAngles = new Vector3(90, 0, 0);
        } 
    }

    void Start()
    {
      
    }
}