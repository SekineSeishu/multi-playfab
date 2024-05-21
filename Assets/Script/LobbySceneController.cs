using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbySceneController : MonoBehaviour
{
    [SerializeField] private LobbyUIManager lobbyUIManager;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance != null)
        {
            string lobbyName = GameManager.Instance.CurrentLobbyName;
            lobbyUIManager.SetLobbyName(lobbyName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
