using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public bool moveHorizontal = true;
    public bool invertMovement = false;
    public float distance = 3f;
    public float speed = 2f;
    private Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float movement = Mathf.PingPong(Time.time * speed, distance);

        if (invertMovement)
        {
            movement = distance - movement;
        }

        if (moveHorizontal)
        {
            transform.position = new Vector3(startPos.x + movement, startPos.y, startPos.z);
        }
        else
        {
            transform.position = new Vector3(startPos.x, startPos.y + movement, startPos.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ink_guy"))
        {
            collision.transform.SetParent(transform);
        }
    }

private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ink_guy"))
        {
            collision.transform.SetParent(null);
        }
    }
}
