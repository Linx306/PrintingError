using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed;

    private Rigidbody2D Rigidbody2D;
    private Vector3 Direction;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = Direction * Speed;
    }
private void OnCollisionEnter2D(Collision2D collision)
{
    Enemy enemy = collision.gameObject.GetComponent<Enemy>();

    if (enemy != null)
    {
        enemy.Hit();
        DestroyBullet();
    }
}
    public void SetDirection(Vector3 direction)
    {
        Direction = direction;

        if (direction.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
    public void DestroyBullet()
    {
        Destroy(gameObject);
    }
}