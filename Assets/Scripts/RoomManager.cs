using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;
    
    public GameObject player;
    [Space]
    public Transform spawnPoint;
    [Space]
    public GameObject roomCamera;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
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
    
    // ReSharper disable Unity.PerformanceAnalysis
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        
        Debug.Log("Joined Room");
        
        roomCamera.SetActive(false);
        SpawnPlayer();

        
    }

    public void SpawnPlayer()
    {
        
        GameObject _player = PhotonNetwork.Instantiate(player.name, spawnPoint.position, Quaternion.identity);
        _player.GetComponent<PlayerSetup>().IsLocalPlayer();    
        _player.GetComponent<HealthManager>().isLocalPlayer = true;    
    }
}
