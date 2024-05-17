using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    
    public PlayerMovement playerMovement;
    public GameObject playerCamera;
    public TextMeshPro nicknameText;


    public string nickname;
    
    public void IsLocalPlayer()
    {
        playerMovement.enabled = true;
        playerCamera.SetActive(true);
    }
    
    [PunRPC]
    public void SetNickname(string _nickname)
    {
        nickname = _nickname;
        nicknameText.text = nickname;
    }
}
