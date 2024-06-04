using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Fusion;

public class LobbyUIManager : MonoBehaviour
{
    public static LobbyUIManager Instance;
    [SerializeField] public List<NetworkObject> playerList;
    [SerializeField] public List<Transform> LobbyPosition;
    [SerializeField] private TMP_Text lobbyNameText;
    [SerializeField] private NetworkObject player;
    [SerializeField] private LobbyState lobbyState;
    [SerializeField] private TMP_Text playerCountText;
    private int playerCount;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
        public void SetLobby(string lobbyName,GameObject playerPrefab,NetworkRunner runner)
    {
        if (lobbyNameText != null && playerPrefab != null)
        {
            lobbyNameText.text = "Lobby Name:" + lobbyName;
            //NetworkObject player = runner.Spawn(playerPrefab, LobbyPosition[playerCount].position, Quaternion.identity, runner.LocalPlayer);
            //player.transform.parent = LobbyPosition[playerCount];
            //playerList.Add(player);
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

    public void UpdateLobby()
    {
        for (int i = 1; i < playerCount; i++)
        {
            playerList[i].transform.parent = null;
            playerList[i].transform.position = LobbyPosition[i].position;
            playerList[i].transform.parent = LobbyPosition[playerCount];
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
