using UnityEngine;
using System.Collections;
using System;
using System.IO;


public class Client : MonoBehaviour
{

    void Start()
    {
	PhotonNetwork.offlineMode = true;
		OnCreatedRoom();
        //PhotonNetwork.ConnectUsingSettings("Alpha V1.000");
    }

    void Update()
    {
    }

    void OnJoinedLobby()
    {
        Debug.Log("Joining Lobby");
        PhotonNetwork.CreateRoom("DevRoom");
    }

    void OnJoinedRoom()
    {
        Debug.Log("Joining Room");

    }
    // For dev, just join the open game
    void OnPhotonCreateRoomFailed()
    {
        PhotonNetwork.JoinRandomRoom();
        GameObject LoadingScreen = new GameObject("LoadingScreen");
        LoadingScreen.AddComponent<LoadingScreen>();
    }
    void OnCreatedRoom()
    {
        // first player in the room
        GameObject newShip = PhotonNetwork.Instantiate("LightCruiser", new Vector3(0, 0, 0),Quaternion.AngleAxis(0, Vector3.left), 0);
        GameObject newShipPhys = PhotonNetwork.Instantiate("LightCruiser_Phys", new Vector3(0, 0, 0), Quaternion.AngleAxis(0, Vector3.left), 0);

        GameObject LoadingScreen = new GameObject("LoadingScreen");
        LoadingScreen.AddComponent<LoadingScreen>();
        LoadingScreen.GetComponent<LoadingScreen>().PlayerShipPhysical = newShip;
        LoadingScreen.GetComponent<LoadingScreen>().PlayerShipVirtual = newShip;
    }
}
