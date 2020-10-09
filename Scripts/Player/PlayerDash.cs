using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    private Rigidbody2D rb;
    public bool IsDashing { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Dash(Vector2 direction, float speed)
    {
        StartCoroutine(DodgeUpdate(direction, speed));
    }

    private IEnumerator DodgeUpdate(Vector2 direction, float speed)
    {
        rb.velocity = direction * speed;
        IsDashing = true;
        yield return new WaitForSeconds(.25f);
        IsDashing = false;
    }
}
