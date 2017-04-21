using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobAnimation : MonoBehaviour {

    public Animation Anim;

	// Use this for initialization
	void Start () {
        Anim = this.GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void playAnimation(string str)
    {
        Anim.Play(str);
    }
}
