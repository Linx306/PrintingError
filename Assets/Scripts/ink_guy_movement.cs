using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ink_guy : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float JumpForce;
    public float Speed;

    public int Life = 5;
    public int MaxLife = 5;
    public Slider healthBar;

    public int MaxAmmo = 5;
    public int CurrentAmmo;
    public Image[] ammoImages;

    public Collider2D swordCollider; 

    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private ink_guy_sounds sounds;

    private float horizontal;
    private bool Grounded;
    private bool isInvulnerable = false;
    private float LastShoot;
    private bool attack;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        sounds = GetComponent<ink_guy_sounds>();

        healthBar.maxValue = MaxLife;
        healthBar.value = Life;

        CurrentAmmo = MaxAmmo;
        UpdateAmmoUI();

        swordCollider.enabled = false; 
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal < 0.0f)
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (horizontal > 0.0f)
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        Animator.SetBool("running", horizontal != 0.0f);

        Grounded = Physics2D.Raycast(transform.position, Vector3.down, 0.2f);
        Animator.SetBool("jumping", !Grounded);

        // SALTO
        if (Input.GetKeyDown(KeyCode.W) && Grounded)
        {
            Jump();
            if (sounds != null) sounds.PlayJump();
        }

        // DISPARO
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > LastShoot + 0.25f && CurrentAmmo > 0)
        {
            Shoot();
            if (sounds != null) sounds.PlayShoot();

            LastShoot = Time.time;
            CurrentAmmo--;
            UpdateAmmoUI();
        }

        // ATAQUE
        if (Input.GetMouseButtonDown(0) && !attack)
        {
            Attack();
        }
    }

    void UpdateAmmoUI()
    {
        for (int i = 0; i < ammoImages.Length; i++)
        {
            ammoImages[i].enabled = i < CurrentAmmo;
        }
    }

    private void Shoot()
    {
        Animator.SetBool("shoot", true);

        Vector3 direction = (transform.localScale.x == 1) ? Vector3.right : Vector3.left;

        GameObject bullet = Instantiate(
            BulletPrefab,
            transform.position + direction * 0.2f,
            Quaternion.identity
        );

        bullet.GetComponent<Bullet>().SetDirection(direction);

        Invoke("StopShoot", 0.3f);
    }

    void StopShoot()
    {
        Animator.SetBool("shoot", false);
    }

    public void Attack()
    {
        attack = true;
        Animator.SetBool("attack", true);

        swordCollider.enabled = true; 

        Invoke("DisableAttack", 0.2f);
    }

    void DisableAttack()
    {
        swordCollider.enabled = false; 
        NoAttack();
    }

    public void NoAttack()
    {
        attack = false;
        Animator.SetBool("attack", false);
    }

    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(horizontal * Speed, Rigidbody2D.velocity.y);
    }

    public void TakeDamage(int damage, Transform enemy)
    {
        if (isInvulnerable) return;

        Life -= damage;
        healthBar.value = Life;

        isInvulnerable = true;

        Animator.SetBool("hurt", true);

        float direction = transform.position.x - enemy.position.x;
        Rigidbody2D.AddForce(new Vector2(direction * 4f, 3f), ForceMode2D.Impulse);

        Invoke("StopHurt", 0.3f);
        Invoke("ResetInvulnerability", 1f);

        if (Life <= 0)
        {
            Destroy(gameObject);

            if (GameManager.Instance != null)
                GameManager.Instance.GameOver();
        }
    }

    void StopHurt()
    {
        Animator.SetBool("hurt", false);
    }

    void ResetInvulnerability()
    {
        isInvulnerable = false;
    }

    public void RestoreDamage(int restore)
    {
        Life = Mathf.Min(Life + restore, MaxLife);
        healthBar.value = Life;
    }

    public void AddAmmo(int amount)
    {
        CurrentAmmo = Mathf.Min(CurrentAmmo + amount, MaxAmmo);
        UpdateAmmoUI();
    }
}