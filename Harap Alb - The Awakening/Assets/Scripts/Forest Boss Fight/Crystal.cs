using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    public float timeUntilDestroy;
    ForestBoss boss;

    private void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Forest Boss").GetComponent<ForestBoss>();
        Destroy(gameObject, timeUntilDestroy);
    }

    
    public void DestroyCrystal()
    {
        // Destroying a crystal always makes the boss take 1/4 of health damage.
        boss.TakeDamage();
        Destroy(gameObject);
    }
}
