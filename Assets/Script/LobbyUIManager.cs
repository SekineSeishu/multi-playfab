using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Fusion;

public class LobbyUIManager : MonoBehaviour
{
    [SerializeField] private List<Transform> LobbyPosition;
    [SerializeField] private TMP_Text lobbyNameText;
    [SerializeField] private Player player;
    public int joinPlayerCount { get; private set; } = 0;

    public void SetLobby(string lobbyName,GameObject playerPrefab)
    {
        if (lobbyNameText != null)
        {
            lobbyNameText.text = "Lobby Name:" + lobbyName;
            GameObject obj = Instantiate(playerPrefab, LobbyPosition[joinPlayerCount]);
            //obj.transform.parent = gameObject.transform;
            joinPlayerCount++;
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
