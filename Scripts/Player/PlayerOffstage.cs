using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOffstage : MonoBehaviour
{
    private float offstageTimer;
    private bool inArena;

    public const float MAX_OFFSTAGE_TIMER = 3.0f;

    public event Action OnInArenaChanged;
    public event Action OnOffstageTimerChanged;
    public event Action OnOffstageTimeMaxed;
    public event Action OnOutOfBounds;

    private void Awake()
    {
        var playerLives = GetComponent<PlayerLives>();
        playerLives.OnDeath += () => enabled = false;
        playerLives.OnRespawn += () =>
        {
            InArena = true;
            enabled = true;
        };
    }
    private void Update()
    {
        if (!inArena) OffstageTimer += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Arena"))
        {
            InArena = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Arena"))
        {
            InArena = false;
        }
        else if (collision.CompareTag("Bounds"))
        {
            Debug.Log("Out of bounds");
            OnOutOfBounds?.Invoke();
        }
    }

    public float OffstageTimer
    {
        get => offstageTimer;
        private set
        {
            offstageTimer = Mathf.Clamp(value, 0, MAX_OFFSTAGE_TIMER);
            if (offstageTimer == MAX_OFFSTAGE_TIMER) OnOffstageTimeMaxed?.Invoke();
            OnOffstageTimerChanged?.Invoke();
        }
    }

    public bool InArena
    {
        get => inArena;
        private set
        {
            inArena = value;
            if (inArena) OffstageTimer = 0;
            OnInArenaChanged?.Invoke();
        }
    }
}
