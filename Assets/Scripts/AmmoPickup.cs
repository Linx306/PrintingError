using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int MinAmmo = 1;
    public int MaxAmmo = 3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ink_guy"))
        {
            ink_guy player = collision.GetComponent<ink_guy>();

            if (player != null)
            {
                int ammo = Random.Range(MinAmmo, MaxAmmo + 1);
                player.AddAmmo(ammo);
                Destroy(gameObject);
            }
        }
    }
}