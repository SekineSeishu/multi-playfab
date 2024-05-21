using CustomConnectionHandler;
using Fusion;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager Instance;
    [SerializeField]
    private NetworkRunner _runner;
    [SerializeField]
    private string lobbySceneName;

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
    public async Task StartPrivateLobby(ConnectionData connectionData)
    {
        string lobbyName = "Lobby" + connectionData.PrivateLobbyPass;
        GameManager.Instance.SetLobbyName(connectionData.PrivateLobbyPass);
        var scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex);
        var sceneInfo = new NetworkSceneInfo();
        if (scene.IsValid)
        {
            sceneInfo.AddSceneRef(scene, LoadSceneMode.Additive);
        }

        var startResult = await _runner.StartGame(new StartGameArgs()
        {
            CustomLobbyName = lobbyName,
            GameMode = GameMode.Shared,
            SessionProperties = null,
            Scene = sceneInfo,
            PlayerCount = 20,
            SceneManager = _runner.GetComponent<INetworkSceneManager>()
        });

        if (startResult.Ok)
        {
            Debug.Log("Private lobby started successfully.");
            _runner.GetComponent<NetworkSceneManager>().LoadScene(lobbySceneName,LoadSceneMode.Single);
        }
        else
        {
            Debug.LogError("Failed to start private lobby: " + startResult.ShutdownReason);
        }
    }

    public async Task JoinPrivateLobby(ConnectionData connectionData)
    {
        var lobbyName = "Lobby" + connectionData.PrivateLobbyPass;
        var joinResult = await _runner.JoinSessionLobby(SessionLobby.Shared,lobbyName);

        GameManager.Instance.SetLobbyName(connectionData.PrivateLobbyPass);
        if (joinResult.Ok)
        {
            Debug.Log("JoinSessionLobby succeeded");
            _runner.GetComponent<NetworkSceneManager>().LoadScene(lobbySceneName, LoadSceneMode.Single);
        }
        else
        {
            Debug.LogError("Failed to join private lobby:" +  joinResult.ShutdownReason);
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
