using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;
using System.Linq;

public class PlayerSpawner : MonoBehaviour, INetworkRunnerCallbacks
{
    //ネットワークランナー
    public NetworkRunner _runner;
    //プレイヤーオブジェクト
    [SerializeField] private GameObject playerPrefab;
    //ロビーキャンバス
    [SerializeField] private Canvas canvas;
    //ロビーのプレイヤーオブジェクトの生成位置リスト
    [SerializeField] private RectTransform[] playerSpawnPositionList;

    void Start()
    {
        _runner = GetComponent<NetworkRunner>();
        _runner.AddCallbacks(this);
    }

    //ロビーでの参加が出来た際の処理
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        //生成位置リストがない場合
        if (playerSpawnPositionList == null || playerSpawnPositionList.Length == 0)
        {
            Debug.LogError("生成ポジションがありません");
            return;
        }

        int playerIndex = _runner.ActivePlayers.Count() - 1;

        if (playerIndex < 0 || playerIndex >= playerSpawnPositionList.Length)
        {
            Debug.LogError("Invalid player index: " + playerIndex);
            return;
        }
        //入ってきた順番に応じた位置にプレイヤーを生成する
        if (player == _runner.LocalPlayer)
        {
            Transform spawnPosition = playerSpawnPositionList[playerIndex];
            NetworkObject playerObject = _runner.Spawn(playerPrefab, spawnPosition.position, Quaternion.identity, player);
        }
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {

    }









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
        
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        
    }
}
