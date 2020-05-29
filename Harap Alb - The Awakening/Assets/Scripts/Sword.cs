using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public GameObject player;

    Vector3 offset;

    private void Start()
    {
        offset = transform.position - player.transform.position;
    }

    private void Update()
    {
        if (player.transform.localScale.x == 1)
        {
            transform.position = offset + player.transform.position;
        }
        else
        {
            transform.position = offset + player.transform.position - new Vector3(0.9f, 0f, 0f);
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Debug.Log("detected collision with" + collision.tag);
        if (collision.gameObject.CompareTag("Caterpillar") && Movement.isAttacking)
        {
            collision.gameObject.GetComponent<Caterpillar>().TakeDamage(player.GetComponent<PlayerStats>().atkPower * 4);
            Movement.isAttacking = false;
            // Debug.Log("detected collision with caterpillar. Took damage: " + player.GetComponent<PlayerStats>().atkPower * 4);
        }
        if (collision.gameObject.CompareTag("Archer") && Movement.isAttacking)
        {
            collision.gameObject.GetComponent<Archer>().TakeDamage(player.GetComponent<PlayerStats>().atkPower * 3);
            Movement.isAttacking = false;
            // Debug.Log("detected collision with caterpillar. Took damage: " + player.GetComponent<PlayerStats>().atkPower * 4);
        }
    }
}
