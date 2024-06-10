using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerSpawner : SimulationBehaviour
{
    public static PlayerSpawner Instance;

    [Networked]
    private Vector3 LocalPosition { get; set; }
    [SerializeField]
    public List<NetworkObject> playerList;
    [SerializeField]
    public List<Transform> playerSpawnPositionList;
    public GameObject PlayerPrefab;

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
    public void PlayerJoined(NetworkRunner Runner)
    {
        //Debug.LogError("aaa");
        playerSpawnPositionList = LobbyUIManager.Instance.LobbyPosition;
        NetworkObject playerObject = Runner.Spawn(PlayerPrefab, playerSpawnPositionList[Runner.SessionInfo.PlayerCount].position, Quaternion.identity);
        playerObject.transform.SetParent(playerSpawnPositionList[Runner.SessionInfo.PlayerCount]);
        LocalPosition = playerObject.transform.localPosition;
        playerList.Add(playerObject);
    }
}
