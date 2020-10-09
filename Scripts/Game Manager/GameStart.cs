using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public event Action OnGameStart;

    int spawnedPlayers;
    private void Awake()
    {
        var spawner = GetComponent<PlayerSpawner>();
        spawner.OnPlayerSpawned += OnPlayerSpawned;
    }

    private void OnPlayerSpawned(PlayerView obj)
    {
        spawnedPlayers++;
        if(spawnedPlayers == 2)
        {
            OnGameStart?.Invoke();
        }
    }

}
