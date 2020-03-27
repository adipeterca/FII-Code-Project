using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    // This sets how much damage the trap does.
    public int damage;
    // We store a PlayerStats reference instead of a gameObject to reduce space.
    public PlayerStats player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // collision.gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
            // Both work the same way
            player.TakeDamage(damage);
        }
    }
}
