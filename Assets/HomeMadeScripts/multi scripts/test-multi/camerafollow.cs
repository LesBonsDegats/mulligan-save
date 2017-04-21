using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerafollow : MonoBehaviour {
    private GameObject target;
    private Vector3 offset;
	// Use this for initialization
	void Start () {
        offset = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		if(target == null)
        {
            target = GameObject.FindGameObjectWithTag("boat");
        }
	}

    private void LateUpdate()
    {
        if (target != null)
        {
            transform.position = new Vector3(target.transform.position.x, target.transform.position.y+offset.y, target.transform.position.z - offset.z);        }
    }
}
