using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;
using Fusion.Sockets;
using System;


public class RoomDataManager : MonoBehaviour, INetworkRunnerCallbacks
{
    private NetworkRunner _runner;
    [SerializeField]
    private List<GameObject> roomDataList;
    public void OnConnectedToServer(NetworkRunner runner)
    {
        throw new NotImplementedException();
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        throw new NotImplementedException();
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        throw new NotImplementedException();
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        throw new NotImplementedException();
    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
        throw new NotImplementedException();
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        throw new NotImplementedException();
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        throw new NotImplementedException();
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
        throw new NotImplementedException();
    }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
        throw new NotImplementedException();
    }

    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
        throw new NotImplementedException();
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        throw new NotImplementedException();
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        throw new NotImplementedException();
    }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {
        throw new NotImplementedException();
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    {
        throw new NotImplementedException();
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
        throw new NotImplementedException();
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
        throw new NotImplementedException();
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
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
    private void Launch(string sessionName)
    {
        Debug.Log($"Launching session: {sessionName}");
        // 実際のセッション開始処理をここに記述
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
