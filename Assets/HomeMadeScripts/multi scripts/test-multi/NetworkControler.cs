using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkControler : MonoBehaviour
{
    private string gameversion = "0.1";
    public GameObject character;
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(gameversion);
    }


    void Update()
    {
        //Debug.Log("Status: " + PhotonNetwork.connectionStateDetailed.ToString());
    }

    void OnJoinedLobby()
    {
        Debug.Log("Trying to join a random room");
        PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("Can't join a room");
        PhotonNetwork.CreateRoom(null);
        Debug.Log("So i create a new room");
    }

    void OnJoinedRoom()
    {
        Debug.Log("Room joined");
        PhotonNetwork.Instantiate("Prefabs/" + character.name, character.transform.position,Quaternion.identity,0);
    }
}