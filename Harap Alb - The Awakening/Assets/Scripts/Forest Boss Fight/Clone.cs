using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone : MonoBehaviour
{
    PlayerStats player;

    public int attack;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        Destroy(gameObject, 5);
    }
    private void Update()
    {
        transform.position -= new Vector3( 5f * Time.deltaTime, 0f, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            player.TakeDamage(attack);
    }
}
