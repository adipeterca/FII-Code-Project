using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    PlayerStats player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        Destroy(gameObject, 10);
    }

    private void Update()
    {
        transform.position += new Vector3(transform.rotation.z == 0f ? 2f : -2f, 0, 0) * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.TakeDamage(25);
            Destroy(gameObject);
        }
        else
            Destroy(gameObject);
    }
}
