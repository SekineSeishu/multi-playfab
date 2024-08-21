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
    //�l�b�g���[�N�����i�[
    [SerializeField] public NetworkRunner _runner;
    //���r�[�R�[�h����͂���I�u�W�F�N�g
    [SerializeField] public GameObject lobbyCodeUI;
    //���r�[
    [SerializeField] private GameObject Lobby;
    //���r�[�Ŏg����UI
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

    //���r�[�̍쐬
    public async Task StartPrivateLobby()
    {
        //�p�����Ń����_����6���̃��r�[�R�[�h���쐬
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

    //���r�[�Q��
    public async Task JoinPrivateLobby(string lobbyCode)
    {
        //���͂������r�[�R�[�h�ƈ�v���郍�r�[��T��
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
