using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestBoss : MonoBehaviour
{
    // This is where he will reappear
    public Transform startPos;

    // Positions for traps/crystal
    public Transform[] trapsPos;
    public Transform[] crystalPos;
    public GameObject trap;
    public GameObject crystal;

    // The clone will handle movement, damage, and when to die
    public Transform chargePos1;   
    public Transform chargePos2;
    public GameObject clone;

    // Boss stats
    // The attack damage will be handled according to the attack type.
    public int health = 100;    // He can only be damaged by the destruction of crystals (1 destroyed crystal = -25% hp)
    public int points;

    // PlayerStats reference
    public PlayerStats player;

    // Colliders
    public BoxCollider2D exit;

    // Fight camera and player camera
    public GameObject bossCamera;
    public GameObject mainCamera;

    Animator anim;

    bool bossActive;
    bool isAttacking = false;

    int attackType;

    // Bools used to check in what phase of the attack are we right now
    bool start = false;
    bool end = false;
    float timeUntilEnd;

    float timeUntilNextAttack = 3f;

    float timeUntilCharge = 3f;
    float timeUntilTrap = 2f;

    private void Start()
    {
        transform.position = startPos.position;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (bossActive == true)
        {
            if (isAttacking == true)    // Is an attack going on right now?
            {
                if (start == true && end == true)   // the attack is complete
                {
                    // wait 3 seconds before starting another attack
                    if (timeUntilNextAttack <= 0f)
                    {
                        timeUntilNextAttack = 3f;
                        isAttacking = false;
                        start = false;
                        end = false;
                    }
                    timeUntilNextAttack -= Time.deltaTime;
                }
                else
                    Attack(attackType);     // continue the attack
            }

            // Both the player and the boss live, so they must fight
            if (health > 0 && player.health > 0 && isAttacking == false)    
            {
                isAttacking = true;
                attackType = Random.Range(1, 3);
                Attack(attackType);
                return;
            }

            // The player won...
            if (health <= 0 && player.health > 0)    
            {
                player.AddPoints(points);
                mainCamera.SetActive(true);
                bossCamera.SetActive(false);
                anim.SetTrigger("Death");
                Destroy(gameObject, 3);
                Destroy(exit.gameObject, 3);
                return;
            }

            // The boss won...
            if (health > 0 && player.health <= 0)    
            {
                bossActive = false;
                health = 100;
                mainCamera.SetActive(true);
                bossCamera.SetActive(false);
                transform.position = startPos.position;
                return;
            }
        }
    }

    private void Attack(int type)
    {
        if (type == 1)  // Do the first attack (traps)
        {
            // start
            if (start == false) // Initiate the attack
            {
                if (timeUntilTrap == 2f)
                {
                    anim.SetTrigger("Trap");
                    timeUntilTrap -= Time.deltaTime;
                    return;
                }
                if (timeUntilTrap > 0f)
                {
                    timeUntilTrap -= Time.deltaTime;
                    return;
                }
                int[] vector = new int[3];
                vector[0] = Random.Range(0, 6);
                vector[1] = Random.Range(0, 6);
                vector[2] = Random.Range(0, 6);

                while (vector[0] == vector[1] || vector[0] == vector[2] || vector[1] == vector[2])
                {
                    vector[0] = Random.Range(0, 6);
                    vector[1] = Random.Range(0, 6);
                    vector[2] = Random.Range(0, 6);
                }

                Instantiate(trap, trapsPos[vector[0]].position, Quaternion.Euler(new Vector3(0, 0, 180)));
                Instantiate(trap, trapsPos[vector[1]].position, Quaternion.Euler(new Vector3(0, 0, 180)));
                Instantiate(trap, trapsPos[vector[2]].position, Quaternion.Euler(new Vector3(0, 0, 180)));
                start = true;
                timeUntilTrap = 2f;
                timeUntilEnd = 0.7f + 1f;    // This must be equal with timeUntilDestroy from Trap script + secondsToWait
                return;
            }
            
            // continue
            if (start == true)
            {
                if (timeUntilEnd <= 0f) // traps are destroyed, now the crystal must spawn
                {
                    // We only instantiate it, the crystal will self destruct from it's script (unless destroyed)
                    Instantiate(crystal, crystalPos[Random.Range(0, crystalPos.Length)].position, new Quaternion());
                    end = true;
                    return;
                }
                timeUntilEnd -= Time.deltaTime;
            }

            // finish
            return;
        }

        if (type == 2) // Do the second attack  (charge)
        {
            if (start == false)
            {
                if (timeUntilCharge == 3f)
                {
                    anim.SetTrigger("Charge");
                    timeUntilCharge -= Time.deltaTime;
                    return;
                }
                if (timeUntilCharge > 0)    // Wait for the animation to finish
                {
                    timeUntilCharge -= Time.deltaTime;
                    return;
                }
                Instantiate(clone, chargePos1.position, new Quaternion());
                Instantiate(clone, chargePos2.position, new Quaternion());
                timeUntilEnd = 5f;  // this must be equal to timeUntilDestroy from the Clone script.
                timeUntilCharge = 3f;
                start = true;
                return;
            }

            if (start == true && end == false)
            {
                if (timeUntilEnd <= 0f)
                {
                    end = true;
                    return;
                }
                timeUntilEnd -= Time.deltaTime;
            }

            return;
        }
    }

    // Helper functions
    
    public void SetBossActive()
    {
        bossActive = true;
    }
    
    public void TakeDamage()
    {
        health -= 25;
        anim.SetTrigger("Damage");
    }

}
