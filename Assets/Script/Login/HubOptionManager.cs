using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HubOptionManager : MonoBehaviour
{
    [SerializeField] private LobbyManager lobbyManager;//���r�[�Ǘ�
    [SerializeField] private TMP_InputField CodeText;//�R�[�h����

    void Start()
    {
        
    }

    //���r�[�����i�{�^���j
    public async void StartLobby()
    {
        await lobbyManager.StartPrivateLobby();
    }

    //���r�[�ɎQ������(�{�^��)
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
