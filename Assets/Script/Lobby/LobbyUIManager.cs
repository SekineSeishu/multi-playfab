using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Fusion;

public class LobbyUIManager : MonoBehaviour
{
    [SerializeField]
    private string _closeLobbyCode;
    public static LobbyUIManager Instance;
    public string _lobbyCode;
    [SerializeField] private TMP_Text lobbyNameText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        lobbyNameText.text = "LobbyCode:" + _closeLobbyCode;
    }

    public void OpenOrCloseLobbyCode()
    {
        if (lobbyNameText.text == "LobbyCode:" + _closeLobbyCode)
        {
            lobbyNameText.text = "LobbyCode:" + _lobbyCode;
        }
        else
        {
            lobbyNameText.text = "LobbyCode:" + _closeLobbyCode;
        }
    }

    void Update()
    {
        
    }
}
