using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbySceneController : MonoBehaviour
{
    [SerializeField] private LobbyUIManager lobbyUIManager;
    private GameObject player;
    [SerializeField] private string homeSceneName = "";
    [SerializeField] private GameObject backHomeUI;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance != null)
        {
            string lobbyName = GameManager.Instance.CurrentLobbyName;
            player = LobbyManager.Instance.playerPrefab;
            NetworkRunner runner = LobbyManager.Instance._runner;
            lobbyUIManager.SetLobby(lobbyName, player);
            //Vector3 spawnPosition = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
            //runner.Spawn(player, spawnPosition, Quaternion.identity, runner.LocalPlayer);
        }
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
