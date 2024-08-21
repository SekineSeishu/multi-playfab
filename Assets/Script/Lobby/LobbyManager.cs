using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager Instance;
    //ネットワークランナー
    [SerializeField] public NetworkRunner _runner;
    //ロビーコードを入力するオブジェクト
    [SerializeField] public GameObject lobbyCodeUI;
    //ロビー
    [SerializeField] private GameObject Lobby;
    //ロビーで使われるUI
    [SerializeField] private LobbyUIManager lobbyUI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this);
    }

    //ロビーの作成
    public async Task StartPrivateLobby()
    {
        //英数字でランダムな6桁のロビーコードを作成
        string lobbyCode = CodeGenerator.GenerateCode(6);
        Debug.Log("LobbyCode:" +  lobbyCode);
        var scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex);
        var startResult = await _runner.StartGame(new StartGameArgs()
        {
            CustomLobbyName = lobbyCode,
            GameMode = GameMode.Shared,
            Scene = scene,
            PlayerCount = 4,
            SceneManager = _runner.GetComponent<INetworkSceneManager>(),
        });

        if (startResult.Ok)
        {
            Debug.Log("Private lobby started successfully.");
            lobbyUI._lobbyCode = lobbyCode;
            Lobby.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            Debug.LogError("Failed to start private lobby: " + startResult.ShutdownReason);
        }
    }

    //ロビー参加
    public async Task JoinPrivateLobby(string lobbyCode)
    {
        //入力したロビーコードと一致するロビーを探す
        var scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex);
        var startResult = await _runner.StartGame(new StartGameArgs()
        {
            CustomLobbyName = lobbyCode,
            GameMode = GameMode.Shared,
            Scene = scene,
            PlayerCount = 4,
            SceneManager = _runner.GetComponent<INetworkSceneManager>(),
        });

        if (startResult.Ok)
        {
            Debug.Log("Private lobby started successfully.");
            lobbyUI._lobbyCode = lobbyCode;
            Lobby.transform.localScale = new Vector3(1, 1, 1);
            lobbyCodeUI.SetActive(false);
        }
        else
        {
            Debug.LogError("Failed to start private lobby: " + startResult.ShutdownReason);
        }
    }
}
