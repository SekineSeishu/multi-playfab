using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;
using Fusion.Sockets;
using System;
using UnityEngine.SceneManagement;


public class RoomDataManager : MonoBehaviour, INetworkRunnerCallbacks
{
    private NetworkRunner _runner;
    [SerializeField]
    private GameObject RoomDataPrefab;
    [SerializeField]
    private GameObject CreateRoomDataPrefab;
    [SerializeField]
    private List<GameObject> roomDataList;
    [SerializeField]
    private Button createButton;
    [SerializeField]
    private Button joinroomButton;

    public void OnConnectedToServer(NetworkRunner runner)
    {
        
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        
    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
        
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
        
    }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
        
    }

    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
        
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        
    }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {
        
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    {
        
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
        
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
        
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        Debug.Log("OnSessionListUpdated called");
        for (int i = 0; i < sessionList.Count; i++)
        {
            var session = sessionList[i];
            roomDataList[i].GetComponent<RoomData>().SetData(session.Properties["ID"].PropertyValue.ToString()
            , session.PlayerCount + "/" + session.MaxPlayers
            , session.Name);
            roomDataList[i].GetComponent<Button>().onClick.RemoveAllListeners();
            roomDataList[i].GetComponent<Button>().onClick.AddListener(() => Launch(session.Name));
            roomDataList[i].SetActive(true);
        }
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        throw new NotImplementedException();
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        throw new NotImplementedException();
    }

    private void Awake()
    {
        _runner = GetComponent<NetworkRunner>();
        Connect();
    }

    private async void Connect()
    {
        _runner.AddCallbacks(this);
        var result = await _runner.JoinSessionLobby(SessionLobby.Shared, "MyCustomLobby");
        if (result.Ok)
        {
            Debug.Log("JoinSessionLobby succeeded");
        }
    }
    public async void CreateRoom(string roomname, int maxPlayers)
    {
        string roomName = roomname;
        if (string.IsNullOrEmpty(roomName))
        {
            Debug.LogError("Room name cannot be empty");
            return;
        }

        var scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex);
        var sceneInfo = new NetworkSceneInfo();
        if (scene.IsValid)
        {
            sceneInfo.AddSceneRef(scene, LoadSceneMode.Additive);
        }

        var startGameArgs = new StartGameArgs()
        {
            GameMode = GameMode.Host,
            SessionName = roomName,
            PlayerCount = maxPlayers,
            Scene = scene,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>(),
        };

        var result = await _runner.StartGame(startGameArgs);
        if (result.Ok)
        {
            Debug.Log($"Room '{roomName}' created successfully");
        }
        else
        {
            Debug.LogError($"Failed to create room: {result.ShutdownReason}");
        }
    }
    private void Launch(string sessionName)
    {
        Debug.Log($"Launching session: {sessionName}");
        // 実際のセッション開始処理をここに記述
    }

    public void CreateMenuOnClick()
    {
        CreateRoomDataPrefab.SetActive(true);
    }
    public void JoinMenuOnClick()
    {
        RoomDataPrefab.SetActive(true);
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
