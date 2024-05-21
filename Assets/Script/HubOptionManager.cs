using CustomConnectionHandler;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HubOptionManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField passwordText;
    [SerializeField] private ConnectionData _initialConnection;
    [SerializeField] private Toggle privateModeToggle;

    [SerializeField] private TMP_InputField setPasswordText;
    // Start is called before the first frame update
    void Start()
    {
        _initialConnection.PrivateLobbyPass = "";
    }

    public void StartSetPassword()
    {
        Guid g = Guid.NewGuid();
        var pass = g.ToString("N").Substring(0, 2);

        passwordText.text = privateModeToggle.isOn ? pass : "";
        _initialConnection.PrivateLobbyPass = pass;
    }

    public void StartLobby()
    {
        _ = LobbyManager.Instance.StartPrivateLobby(_initialConnection);
    }
    public void JoinLobby()
    {
         _initialConnection.PrivateLobbyPass = passwordText.text;
        _ = LobbyManager.Instance.StartPrivateLobby(_initialConnection);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
