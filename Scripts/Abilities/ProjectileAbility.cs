using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public struct OnAbilityHitEventArgs
{
    public AbilityView abilityView;
    public GameObject hit;
    public Vector2 hitDirection;
    public float damage;
    public float knockback;
}

public class ProjectilePool
{
    private PlayerView player;
    private List<ProjectileView> projectilePool = new List<ProjectileView>();
    private int poolIndex;
    public ProjectileView CurrentProjectile => projectilePool[poolIndex % projectilePool.Count];

    public ProjectilePool(PlayerView player, string projectileName)
    {
        this.player = player;
        for (int i = 0; i < 3; i++)
        {
            var projectile = PhotonNetwork.Instantiate("Projectiles/" + projectileName, Vector3.zero, Quaternion.identity).GetComponent<ProjectileView>();
            projectile.Init(OnProjectileHit);
            projectilePool.Add(projectile);
        }
    }

    public void Shoot(Vector2 targetPosition, float speed, float damage, float knockback, float size = 1)
    {
        var velocity = targetPosition - (Vector2)player.transform.position;
        CurrentProjectile.Shoot(player.transform.position, velocity.normalized * speed, damage, knockback, size);
        poolIndex++;
    }

    private void OnProjectileHit(OnAbilityHitEventArgs args)
    {
        var player = args.hit.GetComponentInParent<PlayerView>();
        if (player && player != this.player)
        {
            player.GetComponent<PlayerKnockback>().Knockback(args.hitDirection.normalized * args.knockback);
            player.GetComponent<PlayerDamage>().Damage(args.damage);

            args.abilityView.SetVisible(false);
        }
    }
}

public class ProjectileAbility : PlayerAbility
{
    protected ProjectilePool projectilePool;
    protected float speed;
    protected float damage;
    protected float knockback;
    protected float size;

    public ProjectileAbility(PlayerView player, AbilityId id, float speed, float damage, float knockback, float size = 1, string projectileName = null) : base(player, id)
    {
        this.speed = speed;
        this.damage = damage;
        this.knockback = knockback;
        this.size = size;
        projectilePool = new ProjectilePool(player, projectileName ?? id.ToString());
    }

    public override void Cast()
    {
        projectilePool.Shoot(GetTargetPosition, speed, damage, knockback, size: size);
    }
}
