using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI healthBar;
    
    public int health = 100;
    public bool isLocalPlayer;

    [PunRPC]
    public void TakeDamage(int damage)
    {
        health -= damage;
        
        healthBar.text = health.ToString();
        
        if (health <= 0)
        {
            if (isLocalPlayer)
            {
                RoomManager.Instance.SpawnPlayer();
                RoomManager.Instance.deaths++;
                RoomManager.Instance.SetHashes();
            }
                
            
         
            
            Destroy(gameObject);
        }
    }
}
