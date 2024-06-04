using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyState : NetworkBehaviour
{
    [Networked] public int JoinPlayerCount { get; private set; } = 0;

    public override void Spawned()
    {
        if (Runner.IsServer)
        {
            JoinPlayerCount = 0; 
        }
    }

    //�v���C���[�̐��𑝂₷
    public void IncrementJoinPlayerCount()
    {
        if (Runner.IsServer)
        {
            JoinPlayerCount++;
        }
    }
}
