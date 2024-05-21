using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LobbyUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text lobbyNameText;

    public void SetLobbyName(string lobbyName)
    {
        if (lobbyNameText != null)
        {
            lobbyNameText.text = "Lobby Name:" + lobbyName;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
