﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubecontroler : MonoBehaviour {
    float speed = 1000f;
    PhotonView view;
	// Use this for initialization
	void Start () {
        view = GetComponent<PhotonView>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

     void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(h, 0.0f, v);
        if(h !=0 || v !=0)
        {
            if(view.isMine)
            {
                GetComponent<Rigidbody>().velocity = movement * speed * Time.deltaTime;
            }
        }
    }
}
