using Photon.Pun;
using System;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class PhotonObject : MonoBehaviourPun
{
    [SerializeField] protected Transform render;

    public void SetVisible(bool isVisible)
    {
        photonView.RPC(nameof(SetVisibleRPC), RpcTarget.AllBuffered, isVisible);
    }

    [PunRPC]
    public void SetVisibleRPC(bool isVisible)
    {
        render.gameObject.SetActive(isVisible);
    }

    public void SetVisible(bool isVisible, Vector2 position, Quaternion rotation)
    {
        photonView.RPC(nameof(SetVisibleRPC), RpcTarget.All, isVisible, position, rotation);
    }

    [PunRPC]
    public void SetVisibleRPC(bool isVisible, Vector2 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
        render.gameObject.SetActive(isVisible);
    }
}

public class ProjectileView : AbilityView
{
    private Rigidbody2D rb;

    public event Action OnProjectileShot;
    public event Action<float> OnSizeChanged;

    public override void Init(Action<OnAbilityHitEventArgs> onHit)
    {
        base.Init(onHit);
        rb = GetComponent<Rigidbody2D>();
    }

    public void Shoot(Vector2 startPosition, Vector2 velocity, float damage, float knockback, float size)
    {
        transform.up = velocity;
        rb.velocity = velocity;
        this.damage = damage;
        this.knockback = knockback;
        transform.localScale = Vector2.one * size;

        SetVisible(true, startPosition, transform.rotation);

        OnProjectileShot?.Invoke();
    }
}
