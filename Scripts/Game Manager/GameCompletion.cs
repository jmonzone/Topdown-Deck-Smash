using System;
using System.Collections.Generic;
using UnityEngine;

public class GameCompletion : MonoBehaviour
{
    public event Action<PlayerView> OnGameComplete;

    private List<PlayerView> alivePlayers = new List<PlayerView>();

    private void Awake()
    {
        var playerSpawner = GetComponent<PlayerSpawner>();
        playerSpawner.OnPlayerSpawned += OnPlayerSpawned;
    }

    private void OnPlayerSpawned(PlayerView player)
    {
        alivePlayers.Add(player);
        player.GetComponent<PlayerLives>().OnOutOfLives += () => OnPlayerDeath(player);
    }

    private void OnPlayerDeath(PlayerView player)
    {
        alivePlayers.Remove(player);
        if (alivePlayers.Count == 1) OnGameComplete?.Invoke(alivePlayers[0]);
    }
}
