using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class RoomDataManager : NetworkBehaviour
{
    private NetworkRunner _runner;
    private void Awake()
    {
        _runner = GetComponent<NetworkRunner>();
        Connect();
    }

    private async void Connect()
    {
        _runner.AddCallbacks((INetworkRunnerCallbacks)this);
        var result = await _runner.JoinSessionLobby(SessionLobby.Shared, "MyCustomLobby");
        if (result.Ok)
        {
            Debug.Log("JoinSessionLobby succeeded");
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
