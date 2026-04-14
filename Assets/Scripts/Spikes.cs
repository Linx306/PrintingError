using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
 public bool instantKill = false; // activar si quieres que mate
    public int damage = 1; // daño si NO es muerte instantánea

    private void OnTriggerEnter2D(Collider2D other)
    {
        ink_guy player = other.GetComponent<ink_guy>();

        if (player != null)
        {
            if (instantKill)
            {
                // Mata directamente
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.GameOver();
                }

                Destroy(other.gameObject);
            }
            else
            {
                // Solo hace daño
                player.TakeDamage(damage, transform);
            }
        }
    }
}
