using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossCardFold : MonoBehaviour {

    public GameObject bossCard;
    public GameObject player;
    public GameObject floor;
    public GameObject THIS;

    public int x;
    public int z;

	// Use this for initialization
	void Start () {
        x = (int)transform.position.x;
        z = (int)transform.position.z;
    }
	
	// Update is called once per frame
	void Update () {

		if  (player.transform.position.x == x && player.transform.position.z == z)
        {
            Instantiate(bossCard, new Vector3(x, 1, z), Quaternion.identity, floor.transform);
            THIS.SetActive(false);
            
        }
	}
}
