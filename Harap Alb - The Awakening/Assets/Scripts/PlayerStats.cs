using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    // The position of the player.
    public float[] position;
    // Skills: max health, attack power and speed.
    public int maxHealth;
    public int atkPower;
    public int speed;
    // Jump power.
    public int jumpPower;
    // Currect health.
    public int health;
    // Lives counter.
    public int lifeCount;
    // Points counter.
    public int points;  // needs a function called addPoints(int amount) which also check to see if the necessary points 
                        // (default 1000) to level up have been collected.
                        // This needs to be done until points < 1000. (A boss could give the player 3000 points)
    public GameObject levelUpMenu;
    public static bool levelUpMenuActive = false;

    public static bool death = false;

    public Text atkText;
    public Text maxHpText;
    public Text speedText;

    private Animator anim;
    
    private void Awake()
    {
        position = new float[3];
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (lifeCount >= 0)
        {
            if (SaveLoadGame.canSave && Input.GetKeyDown(KeyCode.P)) 
            {
                position[0] = transform.position.x;
                position[1] = transform.position.y;
                position[2] = transform.position.z;
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (levelUpMenuActive == true)
                {
                    levelUpMenu.SetActive(false);
                    levelUpMenuActive = false;
                }
                else if (levelUpMenuActive == false && anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Idle"))
                {
                    levelUpMenu.SetActive(true);
                    levelUpMenuActive = true;
                    UpdateStats();
                }
            }
        }
        // else EndGame();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PickUp"))
        {
            AddPoints(collision.gameObject.GetComponent<EXP_Stats>().points);
            Destroy(collision.gameObject);
        }
    }

    public void AddPoints(int amount)
    {
        points += amount;
    }

    public void IncreaseAttack()
    {
        if (points < 1000)
            return;
        atkPower++;
        points -= 1000;
        UpdateStats();
    }

    public void IncreaseMaxHp()
    {
        if (points < 1000)
            return;
        maxHealth += 50;
        points -= 1000;
        UpdateStats();
    }

    public void IncreaseSpeed()
    {
        if (points < 1000)
            return;
        speed++;
        points -= 1000;
        UpdateStats();
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            death = true;
            // updateLifeCount();
            // anim.Death();
        }
    }

    void UpdateStats()
    {
        atkText.text = atkPower.ToString();
        maxHpText.text = (maxHealth / 50).ToString();
        speedText.text = speed.ToString();
    }
}
