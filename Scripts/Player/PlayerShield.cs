using Photon.Pun;
using UnityEngine;

public class PlayerShield : MonoBehaviourPun
{
    //todo move to view script
    [SerializeField] private Transform shield;

    private float shieldHealth;
    private float ShieldHealth
    {
        get => shieldHealth;
        set
        {
            shieldHealth = Mathf.Clamp(value, 0, MAX_SHIELD_HEALTH);
            shield.transform.localScale = Vector3.one * 0.5f * (shieldHealth / MAX_SHIELD_HEALTH);
            if (shieldHealth == 0) SetShield(false);
        }
    }

    public bool IsShielding { get; private set; }

    private const float MAX_SHIELD_HEALTH = 10f;

    private void Awake()
    {
        var dodge = GetComponent<PlayerDodge>();
        dodge.OnDodge += () => SetShield(false);
        ShieldHealth = MAX_SHIELD_HEALTH;
    }

    private void Update()
    {
        if (IsShielding) ShieldHealth -= Time.deltaTime * 2.0f;
        else ShieldHealth += Time.deltaTime;
    }

    public void DamageShield(float damage)
    {
        photonView.RPC(nameof(SetShieldRPC), RpcTarget.All, IsShielding);
    }

    [PunRPC]
    public void DamageShieldRPC(float damage)
    {
        var shieldDamage = damage / 10;
        ShieldHealth -= shieldDamage;
    }

    public void SetShield(bool isShielding)
    {
        if (isShielding && ShieldHealth < 0.5f) return;

        photonView.RPC(nameof(SetShieldRPC), RpcTarget.All, isShielding);
    }

    [PunRPC]
    public void SetShieldRPC(bool isShielding)
    {
        IsShielding = isShielding;
        shield.gameObject.SetActive(isShielding);
    }
}
