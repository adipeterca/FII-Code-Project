using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { left = -1, right = 1 };

public class Archer : MonoBehaviour
{
    public int health;
    public int attack;
    public int points = 200;

    public Direction direction;

    public float timeBetweenAttacks;

    public GameObject arrow;

    Animator anim;
    // Used to count time passed between ranged attacks.
    float time;
    // Used to count how often the nearby player is damaged.
    float time2;
    // Used to instantiane the arrow at the right time.
    float time3 = 1.25f;

    private void Awake()
    {
        transform.localScale = new Vector3((int)direction * transform.localScale.x, transform.localScale.y, transform.localScale.z);
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (health <= 0)
            return;
        if (time <= 0 && anim.GetBool("Death") == false && health > 0)
        {
            anim.SetTrigger("Attack");
            time = timeBetweenAttacks;
        }
        if (time3 <= 0)
        {
            Instantiate(arrow, new Vector3(transform.position.x + (int)direction * 0.65f, transform.position.y + 0.35f, 0), Quaternion.Euler(0f, 0f, ((int)direction == 1 ? 0f : 180f)));
            time3 = timeBetweenAttacks;
        }
        time -= Time.deltaTime;
        time2 -= Time.deltaTime;
        time3 -= Time.deltaTime;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject, 5);
            GetComponent<CapsuleCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            anim.SetBool("Death", true);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().AddPoints(points);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && health > 0 && time2 <= 0)
        {
            collision.GetComponent<PlayerStats>().TakeDamage(attack);
            time2 = 1;
        }
    }
}
