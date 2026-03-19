using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ink_guy : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float JumpForce;
    public float Speed;
    public int Life = 5;
    public Slider healthBar;
    public int MaxLife = 5;
    public int MaxAmmo = 5;
    public int CurrentAmmo;
    public Image[] ammoImages;
    private Rigidbody2D Rigidbody2D;
    private Animator Animator;   
     private float horizontal;
    private bool Grounded;

    private float LastShoot;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        healthBar.maxValue = MaxLife;
        healthBar.value = Life;
        CurrentAmmo = MaxAmmo;
        UpdateAmmoUI();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if(horizontal < 0.0f) transform.localScale = new Vector3(-1.0f,1.0f,1.0f);
        else if (horizontal > 0.0f) transform.localScale = new Vector3(1.0f,1.0f,1.0f);

        Animator.SetBool("running", horizontal !=0.0f);

        Debug.DrawRay(transform.position, Vector3.down * 0.2f, Color.red);
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.2f))
{
    Grounded = true;
}
else
{
    Grounded = false;
}

Animator.SetBool("jumping", !Grounded);
        
        if (Input.GetKeyDown(KeyCode.W) && Grounded)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > LastShoot + 0.25f && CurrentAmmo > 0)
        {
            Shoot();
            LastShoot = Time.time;
            CurrentAmmo--;
            UpdateAmmoUI();
        }



    }

    void UpdateAmmoUI()
{
    for (int i = 0; i < ammoImages.Length; i++)
    {
        if (i < CurrentAmmo)
        {
            ammoImages[i].enabled = true; // visible
        }
        else
        {
            ammoImages[i].enabled = false; // oculto
        }
    }
}

    private void Shoot()
    {
         Vector3 direction;
        if(transform.localScale.x == 1.0f) direction = Vector3.right;
        else direction = Vector3.left;
       GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.2f,Quaternion.identity);
       bullet.GetComponent<Bullet>().SetDirection(direction);
    }
    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }
    private void FixedUpdate()
    {
       Rigidbody2D.velocity = new Vector2(horizontal * Speed, Rigidbody2D.velocity.y);
    }
public void TakeDamage(int damage)
{
    Life -= damage;
    healthBar.value = Life;

    if (Life <= 0)
    {
        Destroy(gameObject);
    }
}

public void AddAmmo(int amount)
{
    CurrentAmmo += amount;

    if (CurrentAmmo > MaxAmmo)
        CurrentAmmo = MaxAmmo;

    UpdateAmmoUI();
}
}
