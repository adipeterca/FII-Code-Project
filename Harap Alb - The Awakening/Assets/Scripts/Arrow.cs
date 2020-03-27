using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Archer enemy;
    public PlayerStats player;

    private void Update()
    {
        transform.position += new Vector3((float)enemy.direction, 0, 0) * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.TakeDamage(enemy.attack);
            Destroy(gameObject);
        }
    }
}
