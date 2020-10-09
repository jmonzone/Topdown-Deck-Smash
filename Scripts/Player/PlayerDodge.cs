using System;
using System.Collections;
using UnityEngine;

public class PlayerDodge : MonoBehaviour
{
    private float timeSinceLastDodge;
    private const float DODGE_COOLDOWN = 1.0f;
    private Rigidbody2D rb;

    public bool IsDodging { get; private set; }
    public event Action OnDodge;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Dodge()
    {
        if (Time.time - timeSinceLastDodge < DODGE_COOLDOWN) return;
        else timeSinceLastDodge = Time.time;

        StartCoroutine(DodgeUpdate());
    }

    private IEnumerator DodgeUpdate()
    {
        var targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var direction = targetPosition - transform.position;
        rb.velocity = direction * 1.5f;
        IsDodging = true;
        OnDodge?.Invoke();
        yield return new WaitForSeconds(.25f);
        IsDodging = false;
    }
}
