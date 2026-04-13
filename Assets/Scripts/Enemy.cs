using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed = 2f;
    public int Life = 3;
    public int Damage = 1;

    public Transform GroundCheck;
    public Transform WallCheck;

    public float GroundCheckDistance = 0.5f;
    public float WallCheckDistance = 0.3f;

    public LayerMask GroundLayer;

    private Rigidbody2D rb;
    private int direction = 1;

    private float lastFlipTime;
    public float flipCooldown = 0.3f;

    private float lastHit;
    public float HitCooldown = 1f;

    private float lastDamageTime;
    public float damageCooldown = 0.3f; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 forward = (direction == 1) ? Vector2.right : Vector2.left;

        RaycastHit2D groundHit = Physics2D.Raycast(
            GroundCheck.position + (Vector3)(forward * 0.2f),
            Vector2.down,
            GroundCheckDistance,
            GroundLayer
        );

        RaycastHit2D wallHit = Physics2D.Raycast(
            WallCheck.position,
            forward,
            WallCheckDistance,
            GroundLayer
        );

        if ((groundHit.collider == null || wallHit.collider != null) && Time.time > lastFlipTime + flipCooldown)
        {
            Flip();
            lastFlipTime = Time.time;
        }

        rb.velocity = new Vector2(direction * Speed, rb.velocity.y);
    }

    void Flip()
    {
        direction *= -1;
        transform.localScale = new Vector3(direction, 1, 1);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ink_guy"))
        {
            if (Time.time > lastHit + HitCooldown)
            {
                ink_guy player = collision.gameObject.GetComponent<ink_guy>();

                if (player != null)
                {
                    player.TakeDamage(Damage, transform);
                    lastHit = Time.time;
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Espada") && Time.time > lastDamageTime + damageCooldown)
        {
            Hit(collision.transform);
            lastDamageTime = Time.time;
        }
    }

    public void Hit(Transform attacker)
    {
        Life--;

        float direction = transform.position.x - attacker.position.x;
        rb.AddForce(new Vector2(direction * 4f, 2f), ForceMode2D.Impulse);

        if (Life <= 0)
        {
            Destroy(gameObject);
        }
    }
}