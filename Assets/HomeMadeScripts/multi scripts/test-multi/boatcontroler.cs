using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boatcontroler : MonoBehaviour {
    public float rotationCoef = 50f;
    public float translationCoef = 10f;
    private PhotonView view;
    private GameObject canon;
    // Use this for initialization
    void Start () {
        view = GetComponent<PhotonView>();
        canon = GameObject.Find("canon");
    }
	
	// Update is called once per frame
	void Update () {
        if (view.isMine)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(Vector3.forward * Time.deltaTime * translationCoef);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(-Vector3.forward * Time.deltaTime * translationCoef);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Rotate(Vector3.up, Time.deltaTime * rotationCoef);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(Vector3.up, -Time.deltaTime * rotationCoef);
            }
            if (Input.GetKey(KeyCode.Space))
            {
                canon.SendMessage("fire");
            }
        }
		
    }
}
