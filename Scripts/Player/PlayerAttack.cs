using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private AbilityDeck abilityDeck;
    private PlayerKnockback playerKnockback;
    private PlayerShield playerShield;
    private PlayerLives playerLives;

    private float timeSinceLastAttack;
    private const float ATTACK_COOLDOWN = 0.75f;

    private float timeSinceLastAbilityStore;
    private const float STORE_COOLDOWN = 0.25f;

    public event Action OnAttack;

    private bool isReleasingAbilities;

    private bool CanAttack => !playerLives.IsDead && !playerShield.IsShielding && !playerKnockback.IsKnockedBack && !isReleasingAbilities && (Time.time - timeSinceLastAttack > ATTACK_COOLDOWN);
    private bool CanStore => !playerLives.IsDead && !isReleasingAbilities && (Time.time - timeSinceLastAbilityStore > STORE_COOLDOWN);

    private void Awake()
    {
        abilityDeck = GetComponent<AbilityDeck>();
        playerKnockback = GetComponent<PlayerKnockback>();
        playerShield = GetComponent<PlayerShield>();
        playerLives = GetComponent<PlayerLives>();

    }

    public void Attack()
    {
        if (!CanAttack) return;

        if (abilityDeck.Attack()) OnAttack?.Invoke();
        else abilityDeck.Reload();
        timeSinceLastAttack = Time.time;
    }

    public void ReleaseAbility()
    {
        if (!CanAttack) return;

        isReleasingAbilities = true;
        abilityDeck.ReleaseStoredAbilities(() => 
        {
            isReleasingAbilities = false;
            timeSinceLastAttack = Time.time;
        });
    }

    public void StoreAbility()
    {
        if (!CanStore) return;

        abilityDeck.StoreAbility();
        timeSinceLastAbilityStore = Time.time;
    }
}
