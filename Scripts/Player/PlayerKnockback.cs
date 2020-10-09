using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockback : MonoBehaviourPun
{
    private Rigidbody2D rb;
    private PlayerDamage playerDamage;
    private PlayerShield playerShield;

    private const float DAMAGE_TAKEN_MULTIPLIER = 0.05f;

    public bool IsKnockedBack { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerDamage = GetComponent<PlayerDamage>();
        playerShield = GetComponent<PlayerShield>();
    }

    public void Knockback(Vector2 knockback)
    {
        if (playerShield.IsShielding) return;

        photonView.RPC(nameof(KnockbackRPC), RpcTarget.All, knockback);
    }

    [PunRPC]
    public void KnockbackRPC(Vector2 knockback)
    {
        if (!photonView.IsMine) return;

        var force = knockback * ((playerDamage.DamageTaken + 1) * DAMAGE_TAKEN_MULTIPLIER);
        StartCoroutine(KnockbackUpdate(force));
    }

    private IEnumerator KnockbackUpdate(Vector2 velocity)
    {
        rb.velocity = velocity;
        IsKnockedBack = true;
        yield return new WaitForSeconds(.25f);
        IsKnockedBack = false;
    }
}
