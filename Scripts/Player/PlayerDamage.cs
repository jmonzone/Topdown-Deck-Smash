using Photon.Pun;
using System;
using UnityEngine;

public class PlayerDamage : MonoBehaviourPun
{
    private PlayerShield playerShield;

    private float damageTaken;
    public float DamageTaken
    {
        get => damageTaken;
        private set
        {
            damageTaken = value;
            OnDamageTakenChanged?.Invoke();
        }
    }

    private float timeSinceLastDamaged;

    public event Action OnDamageTakenChanged;
    public event Action OnDamaged;

    private void Awake()
    {
        playerShield = GetComponent<PlayerShield>();

        var playerLives = GetComponent<PlayerLives>();
        playerLives.OnRespawn += () => SetDamageTaken(0);
    }

    public void Damage(float damage)
    {
        if (Time.time - timeSinceLastDamaged < 0.1f) return;

        if (playerShield.IsShielding) return;
        photonView.RPC(nameof(DamageRPC), RpcTarget.All, damage);
    }

    [PunRPC]
    public void DamageRPC(float damage)
    {
        DamageTaken += damage;
        //if(photonView.IsMine) OnDamaged?.Invoke();
        timeSinceLastDamaged = Time.time;
    }

    private void SetDamageTaken(float damage)
    {
        photonView.RPC(nameof(SetDamageTakenRPC), RpcTarget.All, damage);
    }

    [PunRPC]
    public void SetDamageTakenRPC(float damage)
    {
        DamageTaken = damage;
    }
}
