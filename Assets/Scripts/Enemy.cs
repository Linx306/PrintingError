using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed = 1f;
    public int Life = 3;
    public int Damage = 1;

    public GameObject ink_guy;

    public Transform GroundCheck;
    public float GroundCheckDistance = 0.5f;

    private Rigidbody2D Rigidbody2D;

    private float lastHit;
    public float HitCooldown = 1f;

    private int moveDirection = 1; // 1 derecha, -1 izquierda

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

   void FixedUpdate()
{
    if (ink_guy == null) return;

    Vector3 forward = (moveDirection == 1) ? Vector3.right : Vector3.left;
    Vector3 origin = GroundCheck.position + forward * 0.2f;

    RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, GroundCheckDistance);
    Debug.DrawRay(origin, Vector2.down * GroundCheckDistance, Color.red);

    if (hit.collider == null)
    {
        Rigidbody2D.velocity = new Vector2(0, Rigidbody2D.velocity.y);
        return;
    }


    float dirToPlayer = ink_guy.transform.position.x - transform.position.x;

    if (Mathf.Abs(dirToPlayer) > 0.1f)
    {
        moveDirection = (dirToPlayer > 0) ? 1 : -1;
    }

    // Movimiento
    Rigidbody2D.velocity = new Vector2(moveDirection * Speed, Rigidbody2D.velocity.y);

    // Voltear sprite
    transform.localScale = new Vector3(moveDirection, 1, 1);
}

    void Flip()
    {
        moveDirection *= -1;
        transform.localScale = new Vector3(moveDirection, 1, 1);
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