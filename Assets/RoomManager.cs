using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public GameObject player;
    [Space]
    public Transform spawnPoint;
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        
        Debug.Log("Connected to Server");
        
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        
        Debug.Log("Joined Lobby");

        PhotonNetwork.JoinOrCreateRoom("test", null, null);
    }
    
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        
        Debug.Log("Joined Room");
        
        GameObject _player = PhotonNetwork.Instantiate(player.name, spawnPoint.position, Quaternion.identity);
        _player.GetComponent<PlayerSetup>().IsLocalPlayer();    
        
      
    }
}
