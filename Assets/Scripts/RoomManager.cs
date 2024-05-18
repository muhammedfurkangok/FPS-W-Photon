using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;
    
    public GameObject player;
    
    [Space]
    public Transform[] spawnPoints;
    
    [Space]
    public GameObject roomCamera;

    [Space]
    public GameObject nickNameScreen;
    public GameObject connectingUI;
    

    private string nickname = "unnamed";

    [HideInInspector]
    public int kills = 0; 
    [HideInInspector]
    public int deaths = 0;

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


    public void ChangeNickname(string _nickname)
    {
        nickname = _nickname;
    }
  
    public void JoinRoomButtonPressed()
    {
        PhotonNetwork.ConnectUsingSettings();
        
        nickNameScreen.SetActive(false);
        connectingUI.SetActive(true);
    }
    private void Start()
    {
        
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
        Transform spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
        
        
        GameObject _player = PhotonNetwork.Instantiate(player.name, spawnPoint.position, Quaternion.identity);
        _player.GetComponent<PlayerSetup>().IsLocalPlayer();    
        _player.GetComponent<HealthManager>().isLocalPlayer = true;  
        
        _player.GetComponent<PhotonView>().RPC("SetNickname", RpcTarget.AllBuffered, nickname);
        PhotonNetwork.LocalPlayer.NickName = nickname;
    }
    public void SetHashes()
    {
        try
        {
            Hashtable hash = PhotonNetwork.LocalPlayer.CustomProperties;
            hash["kills"] = kills;
            hash["deaths"] = deaths;
            
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);

        }
        catch
        {
            Debug.Log("Error setting hashes");
        }
    }
}
