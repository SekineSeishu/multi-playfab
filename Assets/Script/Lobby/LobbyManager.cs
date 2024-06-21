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
    public NetworkRunner _runner;
    [SerializeField]
    public GameObject playerPrefab;
    [SerializeField] 
    public GameObject lobbyCodeUI;
    [SerializeField]
    private GameObject Lobby;
    [SerializeField]
    private LobbyUIManager lobbyUI;

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
    public async Task StartPrivateLobby(/*ConnectionData connectionData*/)
    {
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
            Lobby.transform.localScale = new Vector3(1,1,1);
        }
        else
        {
            Debug.LogError("Failed to start private lobby: " + startResult.ShutdownReason);
        }
    }

    public async Task JoinPrivateLobby(string lobbyCode)
    {
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
