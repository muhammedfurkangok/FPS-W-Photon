using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class RoomList : MonoBehaviourPunCallbacks
{

    public static RoomList Instance;
    [Header("UI")]
    public Transform roomListParent;

    public GameObject RoomManagerGameObject;
    public RoomManager RoomManager;
    
    public GameObject roomListItemPrefab;

    private List<RoomInfo> cachedRoomList = new List<RoomInfo>();

    private IEnumerator Start()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.Disconnect();
        }
        
        yield return new WaitUntil(() => !PhotonNetwork.IsConnected);
        
        PhotonNetwork.ConnectUsingSettings();
    }

    public void ChangeRommToCreateName( string _name)
    {
        RoomManager.roomNameToJoin = _name;
    }
    
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

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        
        PhotonNetwork.JoinLobby();
        
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (cachedRoomList.Count <= 0)
        {
            cachedRoomList = roomList;
        }
        else
        {
            foreach (var room in roomList)
            {
                for (int i = 0; i < cachedRoomList.Count; i++)
                {
                    if(cachedRoomList[i].Name == room.Name)
                    {
                        List<RoomInfo> newList = new List<RoomInfo>();
                        
                        if(room.RemovedFromList)
                        {
                          newList.Remove(newList[i]);
                        }
                        else
                        {
                            newList[i] = room;
                        }
                        
                        cachedRoomList = newList;
                    }
                }  
            }
        }
        UpdateUI();
    }

    void UpdateUI()
    {
        foreach (Transform roomItem in roomListParent)
        {
            Destroy(roomItem.gameObject);
        }

        foreach (var room in cachedRoomList)
        {
            GameObject roomItem = Instantiate(roomListItemPrefab, roomListParent);
            
            roomItem.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = room.Name;
            roomItem.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = room.PlayerCount + "/16";

            roomItem.GetComponent<RoomItemButtonController>().RoomName = room.Name;
        }
    }

    public void JoinRoomByName(string _name)
    {
        RoomManager.roomNameToJoin = _name;
        RoomManagerGameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
