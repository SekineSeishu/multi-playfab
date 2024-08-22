using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HubOptionManager : MonoBehaviour
{
    [SerializeField] private LobbyManager lobbyManager;//ロビー管理
    [SerializeField] private TMP_InputField CodeText;//コード入力

    void Start()
    {
        
    }

    //ロビーを作る（ボタン）
    public async void StartLobby()
    {
        await lobbyManager.StartPrivateLobby();
    }

    //ロビーに参加する(ボタン)
    public async void JoinLobby()
    {
        string lobbyCode = CodeText.text;
        if (string.IsNullOrEmpty(lobbyCode))
        {
            Debug.LogError("Lobby Code is empty");
            return;
        }
        await lobbyManager.JoinPrivateLobby(lobbyCode);
    }
    void Update()
    {
        
    }
}
