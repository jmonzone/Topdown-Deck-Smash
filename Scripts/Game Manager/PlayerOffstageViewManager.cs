using UnityEngine;

public class PlayerOffstageViewManager : MonoBehaviour
{
    [SerializeField] private PlayerOffstageView offstageViewPrefab;
    [SerializeField] private Transform offstageViewParent;

    private void Awake()
    {
        var playerSpawner = GetComponent<PlayerSpawner>();
        playerSpawner.OnPlayerSpawned += OnPlayerSpawned;
    }

    private void OnPlayerSpawned(PlayerView player)
    {
        var offstageView = Instantiate(offstageViewPrefab, offstageViewParent);
        offstageView.Init(player);
    }
}
