using Photon.Pun;
using System;
using UnityEngine;

public abstract class AbilityView : PhotonObject
{
    protected Action<OnAbilityHitEventArgs> onHit;
    protected float damage;
    protected float knockback;

    public virtual void Init(Action<OnAbilityHitEventArgs> onHit)
    {
        this.onHit = onHit;
        SetVisible(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var args = new OnAbilityHitEventArgs()
        {
            abilityView = this,
            hit = collision.gameObject,
            hitDirection = collision.transform.position - transform.position,
            damage = damage,
            knockback = knockback,
        };

        onHit?.Invoke(args);
    }
}

public class WeaponView : AbilityView
{
    private Animator animator;
    private Transform followTarget;
    private float timeSinceLastAttack;
    public int ComboIndex { get; private set; }

    private const float COMBO_TIMER = 1.5f;

    public event Action<int> OnAttack;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Init(Action<OnAbilityHitEventArgs> onHit, Transform followTarget)
    {
        this.followTarget = followTarget;
        base.Init(onHit);
    }

    private void Update()
    {
        if(photonView.IsMine) transform.position = followTarget.position;
    }

    public void Attack(Vector2 direction)
    {
        if (Time.time - timeSinceLastAttack > COMBO_TIMER) ComboIndex = 0;
        else ComboIndex = (ComboIndex + 1) % 3;
        
        transform.up = direction;

        photonView.RPC(nameof(AttackRPC), RpcTarget.All, ComboIndex);

        timeSinceLastAttack = Time.time;
    }

    [PunRPC]
    public void AttackRPC(int comboIndex)
    {
        render.gameObject.SetActive(true);
        
        animator.SetInteger("Combo Index", comboIndex);
        animator.SetTrigger("Attack");

        OnAttack?.Invoke(comboIndex);

    }
}
