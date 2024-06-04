using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Fusion;

public class LobbyUIManager : MonoBehaviour
{
    [SerializeField] private List<Transform> LobbyPosition;
    [SerializeField] private TMP_Text lobbyNameText;
    [SerializeField] private NetworkObject player;
    [SerializeField] private LobbyState lobbyState;
    [SerializeField] private TMP_Text playerCountText;
    private int playerCount;

    public void SetLobby(string lobbyName,GameObject playerPrefab,NetworkRunner runner)
    {
        if (lobbyNameText != null && playerPrefab != null)
        {
            lobbyNameText.text = "Lobby Name:" + lobbyName;
            player = runner.Spawn(playerPrefab, LobbyPosition[playerCount].position, Quaternion.identity, runner.LocalPlayer);
            player.transform.parent = LobbyPosition[playerCount];
            //GameObject obj = Instantiate(playerPrefab, LobbyPosition[lobbyState.JoinPlayerCount].transform.position,Quaternion.identity);
            //_.transform.parent = LobbyPosition[lobbyState.JoinPlayerCount];
        }
    }

    public void UpdatePlayerCount(int count)
    {
        if (playerCountText != null)
        {
            playerCountText.text = "Players: " + count;
            playerCount = count;
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
