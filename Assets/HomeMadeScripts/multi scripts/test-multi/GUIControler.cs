using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIControler : MonoBehaviour {
    Text statustext;
    Text mastertext;

    void Start ()
    {
        statustext = GameObject.Find("statustext").GetComponent<Text>();
        mastertext = GameObject.Find("mastertext").GetComponent<Text>();
	}
	

	void Update () {
        statustext.text = "Status: " + PhotonNetwork.connectionStateDetailed.ToString();
        mastertext.text = "Is master client: " + PhotonNetwork.isMasterClient.ToString();
    }
}
