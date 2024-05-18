using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using TMPro;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    public GameObject PlayerHolder;

    [Header("Options")] public float refreshRate = 1f;

    [Header("UI")] public GameObject[] slots;

    [Space] public TextMeshProUGUI[] scoreTexts;
    public TextMeshProUGUI[] nicknameTexts;
    public TextMeshProUGUI[] kdTexts;

    private void Start()
    {
        InvokeRepeating(nameof(Refresh), 1f, refreshRate);
    }

    private void Refresh()
    {
        foreach (var slot in slots)
        {
            slot.SetActive(false);
        }

        var sortedPlayerList = (from player in PhotonNetwork.PlayerList
            orderby player.GetScore() descending
            select player).ToList();

        int i = 0;

        foreach (var player in sortedPlayerList)
        {
            slots[i].SetActive(true);

            if (player.NickName == "")
                player.NickName = "unnamed";

            nicknameTexts[i].text = player.NickName;
            scoreTexts[i].text = player.GetScore().ToString();

            i++;

            if (player.CustomProperties["kills"] != null)
            {
                kdTexts[i].text = player.CustomProperties["kills"] + "/" + player.CustomProperties["deaths"];
            }
            else
            {
                kdTexts[i].text = "0/0";

            }
        }



    }

    private void Update()
    {
      PlayerHolder.SetActive(Input.GetKey(KeyCode.Tab));
    }
}

