using Photon.Pun;
using UnityEngine;

public class WeaponAbility : PlayerAbility
{
    private readonly WeaponView weapon;
    private PlayerDash playerDash;

    private float damage = 7;
    private float finalDamage = 10f;
    private float knockback = 2.5f;
    private float finalKnockback = 5f;

    public WeaponAbility(PlayerView player, AbilityId id) : base(player, id)
    {
        weapon = PhotonNetwork.Instantiate("Tree", Vector3.zero, Quaternion.identity).GetComponent<WeaponView>();
        weapon.Init(OnWeaponHit, player.transform);

        playerDash = player.GetComponent<PlayerDash>();
    }

    public override void Cast()
    {
        var targetPosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var direction = targetPosition - (Vector2)player.transform.position;
        var dashSpeed = weapon.ComboIndex == 2 ? 3f : 5f;
        
        playerDash.Dash(direction.normalized, dashSpeed);
        weapon.Attack(GetTargetDirection);
    }

    private void OnWeaponHit(OnAbilityHitEventArgs args)
    {
        var player = args.hit.GetComponentInParent<PlayerView>();
        if (player && player != this.player)
        {
            var kb = weapon.ComboIndex == 2 ? finalKnockback : knockback;
            player.GetComponent<PlayerKnockback>().Knockback(args.hitDirection.normalized * kb);

            var dmg = weapon.ComboIndex == 2 ? finalDamage : damage;
            player.GetComponent<PlayerDamage>().Damage(dmg);
        }

    }
}
