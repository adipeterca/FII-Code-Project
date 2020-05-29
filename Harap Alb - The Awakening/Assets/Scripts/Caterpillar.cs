using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caterpillar : MonoBehaviour
{
    public int health;
    public int attack;
    public int points;

    // The start and end points for movement (enemy is going from startPoint to endPoint and back).
    public Transform startPoint;
    public Transform endPoint;

    Animator anim;
    PlayerStats player;
    BoxCollider2D col;

    float timeBetweenAttacks = 0f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        transform.position = startPoint.position;

        if (startPoint.position.x < endPoint.position.x)
            transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private void Update()
    {
        if (health <= 0 && points != -1)
        {
            player.AddPoints(points);
            points = -1;    // We don't want to add points more than once
            anim.SetTrigger("Death");
            col.enabled = false;
            
            Destroy(gameObject, 3);
            return;
        }

        if (health <= 0)
            return;
        if (startPoint.position.x < endPoint.position.x)
        {
            transform.position += (new Vector3(1, 0, 0) * Time.deltaTime);
        }
        else
        {
            transform.position += (new Vector3(-1, 0, 0) * Time.deltaTime);
        }

        if ((transform.position.x >= endPoint.position.x && startPoint.position.x < endPoint.position.x) ||
             (transform.position.x <= endPoint.position.x && startPoint.position.x > endPoint.position.x))
        {
            transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
            Vector3 aux = startPoint.position;
            startPoint.position = endPoint.position;
            endPoint.position = aux;
        }
        timeBetweenAttacks -= Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && timeBetweenAttacks <= 0f)
        {
            collision.GetComponent<PlayerStats>().TakeDamage(attack);
            timeBetweenAttacks = 1f;
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
    }
}
