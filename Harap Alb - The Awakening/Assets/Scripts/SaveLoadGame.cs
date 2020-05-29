using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script loads the game and also deals with calling the SaveGame script (based on collisions).
public class SaveLoadGame : MonoBehaviour
{
    public PlayerStats playerStats;
    public Transform playerTransform;
    public Animator playerAnim;

    public GameObject deathState;

    // Can the player save? (Is him near a campfire?)
    public static bool canSave = false;

    // Time to wait after death so that the animations have time to play.
    private float timeToWaitAfterDeath = 6.5f;

    private void Start()
    {
        PlayerData data = SaveGame.Load();

        playerTransform.position = new Vector3(data.position[0], data.position[1], data.position[2]);

        playerStats.maxHealth = data.maxHp;
        playerStats.atkPower = data.atkPower;
        playerStats.speed = data.speed;

        playerStats.lifeCount = data.lives;
        playerStats.health = data.currentHp;

        playerStats.points = data.points;
        // Debug.Log("Loaded a previous save!");
    }

    void Update()
    {
        if (PauseMenu.gameIsPaused || CameraMovement.GetMinimapStatus() == true)
            return;

        // The player wishes to SAVE the game
        if (Input.GetKeyDown(KeyCode.P) && canSave && !PlayerStats.death)
        {
            playerStats.position[0] = playerTransform.position.x;
            playerStats.position[1] = playerTransform.position.y;
            playerStats.position[2] = playerTransform.position.z;

            SaveGame.Save(playerStats);
            canSave = false;
            // Debug.Log("Saved game.");
        }
        else
        {
            // The player wishes to LOAD the game
            if (Input.GetKeyDown(KeyCode.L) && !PlayerStats.death)  // The same as you would load a previous save
            {
                PlayerData data = SaveGame.Load();

                playerTransform.position = new Vector3(data.position[0], data.position[1], data.position[2]);

                playerStats.maxHealth = data.maxHp;
                playerStats.atkPower = data.atkPower;
                playerStats.speed = data.speed;

                playerStats.lifeCount = data.lives;
                playerStats.health = data.currentHp;

                playerStats.points = data.points;
                // Debug.Log("Loaded game by pressing L.");
            }
            else
            {
                if (PlayerStats.death) // This only happends if the player dies
                {
                    if (timeToWaitAfterDeath > 0)
                    {
                        timeToWaitAfterDeath -= Time.deltaTime;
                        return;
                    }
                    // Loads the game to the last save at a campfire
                    PlayerData data = SaveGame.Load();

                    // Respawn at the campfire
                    playerTransform.position = new Vector3(data.position[0], data.position[1], data.position[2]);

                    playerStats.lifeCount = data.lives - 1;
                    playerStats.health = data.maxHp + 100;

                    SaveGame.Save(playerStats);

                    PlayerStats.death = false;
                    timeToWaitAfterDeath = 6.5f;
                    deathState.SetActive(false);
                    playerAnim.SetBool("IsDead", false);
                    // Debug.Log("You died.");
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Campfire"))
            canSave = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Campfire"))
            canSave = false;
    }
}
