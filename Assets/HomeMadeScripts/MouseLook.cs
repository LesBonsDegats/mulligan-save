﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class MouseLook : MonoBehaviour
    {

        public GameObject camera;

        public float sensitivityX = 15F;
        public float sensitivityY = 15F;

        public float minimumX = -360F;
        public float maximumX = 360F;

        public float minimumY = -60F;
        public float maximumY = 60F;

        float rotationX = 0F;
        float rotationY = 0F;
       
        Quaternion originalRotation;
        Quaternion originalRotationCam;

         public PauseMenu pm;

        void Update()
        {


        if (!pm.isActive)
        {
            // Read the mouse input axis
            rotationX += Input.GetAxis("Mouse X") * sensitivityX;
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;



            rotationX = ClampAngle(rotationX, minimumX, maximumX);
            rotationY = ClampAngle(rotationY, minimumY, maximumY);

            Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);

            transform.localRotation = originalRotation * xQuaternion;
            camera.transform.localRotation = originalRotation * yQuaternion;

        }
        }

        void Start()
        {
            // Make the rigid body not change rotation
           /* if (rigidbody)
                rigidbody.freezeRotation = true;
                */
            originalRotation = transform.localRotation;
            originalRotationCam = camera.transform.localRotation;
        }

        public static float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360F)
                angle += 360F;
            if (angle > 360F)
                angle -= 360F;
            return Mathf.Clamp(angle, min, max);
        }
    }