using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tinta : MonoBehaviour
{
    public GameObject ink_guy;
    public int restore;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ink_guy"))
        {
            ink_guy player = collision.GetComponent<ink_guy>();

            if (player != null)
            {
                player.RestoreDamage(restore);
                Destroy(gameObject);
            }
        }
    }
}
