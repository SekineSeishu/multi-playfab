using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbySceneController : MonoBehaviour
{
    [SerializeField] private LobbyUIManager lobbyUIManager;
    private GameObject player;
    private NetworkRunner runner;
    private string lobbyName;
    [SerializeField] private string homeSceneName = "";
    [SerializeField] private GameObject backHomeUI;
    [SerializeField] private LobbyState lobbyState;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance != null)
        {
            lobbyName = GameManager.Instance.CurrentLobbyName;
            player = LobbyManager.Instance.playerPrefab;
            runner = LobbyManager.Instance._runner;
            OnPlayerJoined(runner);
            lobbyUIManager.SetLobby(lobbyName, player, runner);
        }
    }
    private void OnPlayerJoined(NetworkRunner runner)
    {
        // プレイヤー数を更新
        lobbyUIManager.UpdatePlayerCount(runner.SessionInfo.PlayerCount);
    }
    public void BackHomeMenu()
    {
        backHomeUI.SetActive(true);
    }

    public void BackLobby()
    {
        backHomeUI.SetActive(false);
    }

    public void BackHome()
    {
        SceneManager.LoadScene(homeSceneName);
        /*Player SelectPlayer = lobbyUIManager.playerList.Find(Player => Player._name == player._name);
        if (SelectPlayer == null)
        {
            Debug.Log("not player");
        }
        //lobbyUIManager.playerList.Remove(SelectPlayer);
        lobbyUIManager.UpdateLobby();*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
