using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;


// Class for photonlobby connection logic
public class PhotonLobby : MonoBehaviourPunCallbacks

{
    public static PhotonLobby lobby;

    public GameObject joinButton;
    public GameObject cancelButton;

    private RoomInfo[] rooms;

    private void Awake()
    {
        lobby = this;
    }


    void Start()
    {
        //Connect to Photon Master Server
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Player has connected to master server");
        PhotonNetwork.AutomaticallySyncScene = true;
        joinButton.SetActive(true);
    }

    public void onJoinButtonClicked()
    {
        joinButton.SetActive(false);
        cancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Connection to random room failed - there must be a room avaible.");
        CreateRoom();
    }

    void CreateRoom()
    {
        Debug.Log("Trying to create a new room.");
        int randomRoomName = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions() {IsVisible = true, IsOpen = true, MaxPlayers = 2};

        PhotonNetwork.CreateRoom("Room" + randomRoomName, roomOps);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Creating Random room failed, there must be a room with the same name.");
        CreateRoom();
    }

    public void OnCancelButtonClicked()
    {
        cancelButton.SetActive(false);
        joinButton.SetActive(true);
        PhotonNetwork.LeaveLobby();
    }
}