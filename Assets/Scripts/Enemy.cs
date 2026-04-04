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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 forward = (direction == 1) ? Vector2.right : Vector2.left;

        // Raycasts
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

        Debug.DrawRay(GroundCheck.position + (Vector3)(forward * 0.2f), Vector2.down * GroundCheckDistance, Color.red);
        Debug.DrawRay(WallCheck.position, forward * WallCheckDistance, Color.blue);

        // 🚨 GIRAR SOLO SI PASÓ UN TIEMPO
        if ((groundHit.collider == null || wallHit.collider != null) && Time.time > lastFlipTime + flipCooldown)
        {
            Flip();
            lastFlipTime = Time.time;
        }

        // Movimiento constante
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
                    player.TakeDamage(Damage);
                    lastHit = Time.time;
                }
            }
        }
    }

    public void Hit()
    {
        Life--;

        if (Life <= 0)
        {
            Destroy(gameObject);
        }
    }
}