using System.Collections.Generic;
using UnityEngine;

public class PlayerPanelViewManager : MonoBehaviour
{
    [SerializeField] private Transform playerPanelParent;

    private List<PlayerPanelView> playerPanels = new List<PlayerPanelView>();
    private int textIndex;

    private void Awake()
    {
        var playerSpawner = GetComponent<PlayerSpawner>();
        playerSpawner.OnPlayerSpawned += OnPlayerSpawned;

        playerPanelParent.GetComponentsInChildren(includeInactive: true, playerPanels);
    }

    private void OnPlayerSpawned(PlayerView player)
    {
        var i = textIndex;
        playerPanels[i].Init(player);
        textIndex++;
    }
}
