using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button_hover : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnMouseEnter()
    {
        this.transform.localScale += new Vector3(0.01f,0.05f,0.0f);
    }
    public void OnMouseExit()
    {
        this.transform.localScale += new Vector3(-0.01f,-0.05f,0f);
    }
}
