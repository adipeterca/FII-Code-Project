using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public int attack;
    public int points;

    public PlayerStats player;

    // The start and end points for movement (enemy is going from startPoint to endPoint and back).
    public Vector3 startPoint;
    public Vector3 endPoint;

    Animator anim;
    Rigidbody2D rb;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        transform.position = startPoint;
    }

    private void Update()
    {
        if (health <= 0)
        {
            player.AddPoints(points);
            anim.SetTrigger("Death");
            Destroy(gameObject, 3);
            return;
        }

        if (transform.position.x >= endPoint.x)
        {
            transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
            Vector3 aux = startPoint;
            startPoint = endPoint;
            endPoint = aux;
        }
        
    }

    private void FixedUpdate()
    {
        if (startPoint.x < endPoint.x)
        {
            transform.position += (new Vector3(1, 0, 0) * Time.deltaTime);
            Debug.Log("Move to the right");
        }
        else
        {
            transform.position += (new Vector3(-1, 0, 0) * Time.deltaTime);
            Debug.Log("Move to the left");
        }
    }


    public void TakeDamage(int amount)
    {
        health -= amount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (health <= 0) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            player.TakeDamage(attack);
        }
    }
}
