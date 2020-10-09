using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviourPunCallbacks
{
    [Range(1,3)]
    [SerializeField] private int numLives = 3;

    public event Action<PlayerView> OnPlayerSpawned;
    public event Action<PlayerView> OnLocalPlayerSpawned;

    private List<AbilityId> Abilities => GameManager.Instance.Abilities;

    private void Start()
    {
        SpawnPlayer();

        for(int i = 0; i < GameManager.Instance.numComputers; i++) SpawnComputer();
    }

    private void SpawnPlayer()
    {
        var spawnPosition = PhotonNetwork.LocalPlayer.ActorNumber == 1 ? Vector2.left : Vector2.right;
        spawnPosition *= 2.0f;

        var player = PhotonNetwork.Instantiate("Player View", spawnPosition, Quaternion.identity).GetComponent<PlayerView>();
        player.Init(Abilities, numLives);
        OnLocalPlayerSpawned?.Invoke(player);
        photonView.RPC(nameof(SpawnPlayerRPC), RpcTarget.AllBuffered, player.photonView.ViewID, PhotonNetwork.NickName);
    }

    private void SpawnComputer()
    {
        var player = PhotonNetwork.Instantiate("Player View", Vector3.right * 2.0f, Quaternion.identity).GetComponent<PlayerView>();
        player.Init(Abilities, numLives);

        var controller = gameObject.AddComponent<AIController>();
        controller.Init(player);

        photonView.RPC(nameof(SpawnPlayerRPC), RpcTarget.All, player.photonView.ViewID, "Computer");
    }

    [PunRPC]
    public void SpawnPlayerRPC(int viewId, string nickname)
    {
        var photonView = PhotonView.Find(viewId);
        var player = photonView.GetComponent<PlayerView>();
        player.Nickname = nickname;
        OnPlayerSpawned?.Invoke(player);
    }
}
