using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLives : MonoBehaviour
{
    private PlayerView player;
    private Rigidbody2D rb;
    private float timeSinceDeath;
    private int lives;

    public bool IsDead { get; private set; }
    public int Lives
    {
        get => lives;
        private set
        {
            lives = value;
            OnLivesChanged?.Invoke(lives);
        }
    }

    public event Action<int> OnLivesChanged;
    public event Action OnRespawn;
    public event Action OnDeath;
    public event Action OnOutOfLives;

    private void Awake()
    {
        player = GetComponent<PlayerView>();
        rb = GetComponent<Rigidbody2D>();

        var offstage = GetComponent<PlayerOffstage>();
        offstage.OnOffstageTimeMaxed += Die;
        offstage.OnOutOfBounds += Die;

        Lives = 3;
    }

    private void Update()
    {
        if (IsDead && Lives > 0 && Time.time - timeSinceDeath > 3.0f) Spawn();
    }

    private void Spawn()
    {
        var spawnPosition = UnityEngine.Random.insideUnitCircle.normalized * 2.0f;
        rb.velocity = Vector2.zero;

        player.SetVisible(true, spawnPosition, Quaternion.identity);

        IsDead = false;
        OnRespawn?.Invoke();
    }

    private void Die()
    {
        if (IsDead) return;
        Debug.Log("Died");

        IsDead = true;
        timeSinceDeath = Time.time;
        Lives -= 1;
        player.SetVisible(false);

        OnDeath?.Invoke();
        if (Lives == 0) OnOutOfLives?.Invoke();
    }

}
