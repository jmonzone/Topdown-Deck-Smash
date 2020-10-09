using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerDodge dodge;
    private PlayerDash dash;
    private PlayerKnockback knockback;

    private const float MOVE_SPEED = 3.0f;
    public bool CanMove => !dodge.IsDodging && !dash.IsDashing && !knockback.IsKnockedBack;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        dodge = GetComponent<PlayerDodge>();
        dash = GetComponent<PlayerDash>();
        knockback = GetComponent<PlayerKnockback>();
    }

    public void Move(Vector2 direction)
    {
        if (!CanMove) return;

        rb.velocity = direction.normalized * MOVE_SPEED;
    }
}
