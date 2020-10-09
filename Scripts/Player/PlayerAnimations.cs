using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        var attack = GetComponent<PlayerAttack>();
        attack.OnAttack += () => animator.SetTrigger("Attack");

        var damage = GetComponent<PlayerDamage>();
        damage.OnDamaged += () => animator.SetTrigger("Damaged");

        var lives = GetComponent<PlayerLives>();
        lives.OnRespawn += () => animator.SetTrigger("Respawn");

    }

    private void Update()
    {
        animator.SetBool("IsWalking", rb.velocity.magnitude > 0);
    }
}
