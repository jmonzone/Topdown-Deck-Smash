using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerSpawner playerSpawner;

    private PlayerView player;
    private PlayerMovement movement;
    private PlayerAttack attack;
    private PlayerShield shield;
    private PlayerDodge dodge;

    private void Awake()
    {
        playerSpawner = GetComponent<PlayerSpawner>();
        playerSpawner.OnLocalPlayerSpawned += OnLocalPlayerSpawned;

        var gameStart = GetComponent<GameStartView>();
        gameStart.OnGameStartComplete += () => enabled = true;

        var gameCompletion = GetComponent<GameCompletion>();
        gameCompletion.OnGameComplete += (a) => enabled = false;

        enabled = false;
    }

    private void OnLocalPlayerSpawned(PlayerView player)
    {
        this.player = player;
        movement = player.GetComponent<PlayerMovement>();
        attack = player.GetComponent<PlayerAttack>();
        shield = player.GetComponent<PlayerShield>();
        dodge = player.GetComponent<PlayerDodge>();
    }

    private void Update()
    {
        var direction = Vector2.zero;

        if (Input.GetKey(KeyCode.W)) direction += Vector2.up;
        if (Input.GetKey(KeyCode.A)) direction += Vector2.left;
        if (Input.GetKey(KeyCode.S)) direction += Vector2.down;
        if (Input.GetKey(KeyCode.D)) direction += Vector2.right;

        movement.Move(direction);
        
        
        if (Input.GetKey(KeyCode.Space) && Input.GetMouseButtonDown(0)) OnSpaceLeftClick();
        else if (Input.GetKeyDown(KeyCode.Space)) OnSpaceDown();
        else if (Input.GetMouseButtonDown(0)) OnLeftClick();

        if (Input.GetKeyUp(KeyCode.Space)) OnSpaceKeyUp();

        if (Input.GetMouseButtonDown(1)) OnRightClick();
    }

    private void OnLeftClick()
    {
        attack.Attack();
    }

    private void OnRightClick()
    {

        if (player.Deck.storedAbilities.Count < 3) attack.StoreAbility();
        else attack.ReleaseAbility();
    }

    private void OnSpaceDown() => shield.SetShield(true);
    private void OnSpaceKeyUp() => shield.SetShield(false);
    private void OnSpaceLeftClick() => dodge.Dodge();
}
