using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;
using System.Linq;

public class PlayerSpawner : MonoBehaviour, INetworkRunnerCallbacks
{
    //�l�b�g���[�N�����i�[
    public NetworkRunner _runner;
    //�v���C���[�I�u�W�F�N�g
    [SerializeField] private GameObject playerPrefab;
    //���r�[�L�����o�X
    [SerializeField] private Canvas canvas;
    //���r�[�̃v���C���[�I�u�W�F�N�g�̐����ʒu���X�g
    [SerializeField] private RectTransform[] playerSpawnPositionList;

    void Start()
    {
        _runner = GetComponent<NetworkRunner>();
        _runner.AddCallbacks(this);
    }

    //���r�[�ł̎Q�����o�����ۂ̏���
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        //�����ʒu���X�g���Ȃ��ꍇ
        if (playerSpawnPositionList == null || playerSpawnPositionList.Length == 0)
        {
            Debug.LogError("�����|�W�V����������܂���");
            return;
        }

        int playerIndex = _runner.ActivePlayers.Count() - 1;

        if (playerIndex < 0 || playerIndex >= playerSpawnPositionList.Length)
        {
            Debug.LogError("Invalid player index: " + playerIndex);
            return;
        }
        //�����Ă������Ԃɉ������ʒu�Ƀv���C���[�𐶐�����
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
