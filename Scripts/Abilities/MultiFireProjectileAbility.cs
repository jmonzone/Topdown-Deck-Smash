using System.Collections;
using UnityEngine;

public class MultiFireProjectileAbility : ProjectileAbility
{
    private readonly int shots;

    public MultiFireProjectileAbility(PlayerView player, AbilityId id, int shots, float speed, float damage, float knockback, string projectileName) : base(player, id, speed, damage, knockback, 1, projectileName)
    {
        this.shots = shots;
    }

    public override void Cast()
    {
        player.StartCoroutine(MultishotUpdate());
    }

    private IEnumerator MultishotUpdate()
    {
        for (int i = 0; i < shots; i++)
        {
            projectilePool.Shoot(GetTargetPosition, speed, damage, knockback, size);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
