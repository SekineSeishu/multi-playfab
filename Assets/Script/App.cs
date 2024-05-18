using CustomConnectionHandler;
using Fusion;
using Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The main purpose of this class is to perform networked operations that the ConnectionManager.cs can't because it's a monobehaviour.
/// </summary>
public class App : NetworkBehaviour
{
    public override void Spawned()
    {
        // Avoid destroying on loading because dungeons doesn't load the scene right away.
        // This object will be automatically destroyed when the runner shutdown.
        DontDestroyOnLoad(this);
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RPC_ShutdownRunner(ConnectionData.ConnectionTarget target)
    {
        InterfaceManager.Instance.ClearInterface();
        var connection = target == ConnectionData.ConnectionTarget.Lobby ? ConnectionManager.Instance.GetLobbyConnection() : ConnectionManager.Instance.GetDungeonConnection();
        if (connection.IsRunning)
            connection.Runner.Shutdown();
    }
}
