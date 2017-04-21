using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCanon : MonoBehaviour {
    private GameObject head;
    public GameObject boulet;
    private float force = 1000f;
    // Use this for initialization
    void Start () {
        head = GameObject.Find("head");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void fire()
    {
        PhotonNetwork.Instantiate("Prefabs/" + boulet.name, head.transform.position, Quaternion.identity, 0);
        Rigidbody body = boulet.GetComponent<Rigidbody>();
        body.AddForce(body.transform.forward * force);
    }
}
