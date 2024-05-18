using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomItemButtonController : MonoBehaviour
{
    public string RoomName;
    
    public void OnButtonPressed()
    {
        RoomList.Instance.JoinRoomByName(RoomName);
    }
}
