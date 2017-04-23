
using UnityEngine;
using System;

public class approachPlayer : MonoBehaviour {


    public GameObject player;
    public int speed;
    public int distanceMin;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool getCommand()
    {
        return (isCloseEnough());
    }

    public bool isCloseEnough()
    {
        float posx = player.transform.position.x;
        float posz = player.transform.position.z;

        float distance = (float)Math.Sqrt((posx - transform.position.x) * (posx - transform.position.x) + (posz - transform.position.z) * (posz - transform.position.z));

        return (distance > distanceMin);
    }
}
