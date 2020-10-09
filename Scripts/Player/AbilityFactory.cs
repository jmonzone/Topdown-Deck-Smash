using System.Collections.Generic;
using UnityEngine;

public enum AbilityId
{
    Tree,
    Arrow,
    Arrow_2,
    Arrow_3,
    Dark_Pulse,
    Dark_Pulse_2,
    Dark_Pulse_3,
}

public class AbilityFactory
{
    private readonly PlayerView player;

    public AbilityFactory(PlayerView player)
    {
        this.player = player;
    }
    public PlayerAbility CreateAbility(AbilityId abilityType)
    {
        PlayerAbility ability;

        switch (abilityType)
        {
            case AbilityId.Tree:
                ability = new WeaponAbility(player, AbilityId.Tree);
                break;
            case AbilityId.Arrow:
                ability = new ProjectileAbility(player, AbilityId.Arrow, speed: 20f, damage: 4f, knockback: 2f);
                break;
            case AbilityId.Dark_Pulse:
                ability = new ProjectileAbility(player, AbilityId.Dark_Pulse, speed: 5f, damage: 7.5f , knockback: 3f, size: 1.5f);
                break;
            default:
                Debug.LogError("Unknown Ability Type.");
                return null;
        }
        return ability;
    }

    public Dictionary<string, PlayerAbility> GetComboAbilities(List<AbilityId> abilityTypes)
    {
        var comboAbilities = new Dictionary<string, PlayerAbility>();
        if (abilityTypes.Contains(AbilityId.Arrow))
        {
            var multishotArrow2 = new MultiFireProjectileAbility(player, AbilityId.Arrow_2, shots: 2, speed: 20f, damage: 5f, knockback: 3f, projectileName: "Arrow");
            comboAbilities.Add("Arrow-Arrow", multishotArrow2);

            var multishotArrow3 = new MultiFireProjectileAbility(player, AbilityId.Arrow_3, shots: 3, speed: 20f, damage: 5f, knockback: 3f, projectileName: "Arrow");
            comboAbilities.Add("Arrow-Arrow-Arrow", multishotArrow3);
        }

        if (abilityTypes.Contains(AbilityId.Dark_Pulse))
        {
            var darkPulse2 = new ProjectileAbility(player, AbilityId.Dark_Pulse_2, speed: 4f, damage: 10f, knockback: 4f, size: 3f, "Dark_Pulse");
            comboAbilities.Add("Dark_Pulse-Dark_Pulse", darkPulse2);

            var darkPulse3 = new ProjectileAbility(player, AbilityId.Dark_Pulse_3, speed: 3f, damage: 12.5f, knockback: 5f, size: 4.5f, "Dark_Pulse");
            comboAbilities.Add("Dark_Pulse-Dark_Pulse-Dark_Pulse", darkPulse3);
        }

        return comboAbilities;
    }
}
