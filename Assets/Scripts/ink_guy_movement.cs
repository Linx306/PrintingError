using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ink_guy : MonoBehaviour
{
    public float JumpForce;
    public float Speed;
    private Rigidbody2D Rigidbody2D;
    private Animator Animator;    private float horizontal;
    private bool Grounded;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if(horizontal < 0.0f) transform.localScale = new Vector3(-1.0f,1.0f,1.0f);
        else if (horizontal > 0.0f) transform.localScale = new Vector3(1.0f,1.0f,1.0f);

        Animator.SetBool("running", horizontal !=0.0f);

        Debug.DrawRay(transform.position, Vector3.down * 0.2f, Color.red);
        if(Physics2D.Raycast(transform.position, Vector3.down, 0.2f))
        {
            Grounded = true;
        }
        else Grounded = false;
        
        if (Input.GetKeyDown(KeyCode.W) && Grounded)
        {
            Jump();
        }
    }
    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }
    private void FixedUpdate()
    {
       Rigidbody2D.velocity = new Vector2(horizontal * Speed, Rigidbody2D.velocity.y);
    }
}
