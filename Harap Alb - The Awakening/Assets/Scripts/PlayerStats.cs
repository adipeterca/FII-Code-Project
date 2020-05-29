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
    public int points;

    public GameObject hurtImage;
    public GameObject deathImage;

    public AudioClip[] audioClip;

    public GameObject levelUpMenu;
    public static bool levelUpMenuActive = false;

    public static bool death = false;

    public Text atkText;
    public Text maxHpText;
    public Text lifeCountText;

    private Animator anim;
    private const int atkMultiplier = 5;
    private AudioSource audioSource;
    
    private void Awake()
    {
        position = new float[3];
        position[0] = transform.position.x;
        position[1] = transform.position.y;
        position[2] = transform.position.z;

        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (lifeCount >= 0)
        {
            if (death == true)
            {

                return;
            }
            if (Input.GetKeyDown(KeyCode.I) && CameraMovement.GetMinimapStatus() == false && PauseMenu.gameIsPaused == false)
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

    // Helper functions

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

    public void IncreaseLifeCount()
    {
        if (points < 3000)
            return;
        lifeCount++;
        points -= 3000;
        UpdateStats();
    }

    public void TakeDamage(int amount)
    {
        if (health <= 0) return;

        health -= amount;
        PlaySound(0);
        hurtImage.SetActive(true);
        if (health <= 0)
        {
            death = true;
            PlaySound(1);
            lifeCount--;
            anim.SetBool("IsDead", true);
            deathImage.SetActive(true);
        }
    }

    void UpdateStats()
    {
        atkText.text = atkPower.ToString();
        maxHpText.text = (maxHealth / 50).ToString();
        lifeCountText.text = lifeCount.ToString();
    }

    private void PlaySound(int soundToPlay)
    {
        audioSource.Stop();
        audioSource.clip = audioClip[soundToPlay];
        audioSource.Play();
    }
}
