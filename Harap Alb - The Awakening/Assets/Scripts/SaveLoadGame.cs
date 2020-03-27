using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script loads the game and also deals with calling the SaveGame script (based on collisions).
public class SaveLoadGame : MonoBehaviour
{
    public PlayerStats playerStats;

    // Can the player save? (Is him near a campfire?)
    public static bool canSave = false;

    void Update()
    {
        if (PauseMenu.gameIsPaused)
            return;

        if (Input.GetKeyDown(KeyCode.P) && canSave && !PlayerStats.death)
        {
            SaveGame.Save(playerStats);
            canSave = false;
            Debug.Log("Saved game.");
        }
        else if(Input.GetKeyDown(KeyCode.L) && !PlayerStats.death)  // The same as you would load a previous save
        {
            PlayerData data = SaveGame.Load();

            transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);

            playerStats.maxHealth = data.maxHp;
            playerStats.atkPower = data.atkPower;
            playerStats.speed = data.speed;

            playerStats.lifeCount = data.lives;
            playerStats.health = data.currentHp;

            playerStats.points = data.points;
            Debug.Log("Loaded game by pressing L.");
        }
        else if (PlayerStats.death) // This only happends if the player dies
        {   
            // Loads the game to the last save at a campfire
            PlayerData data = SaveGame.Load();

            // Respawn at the campfire
            transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);

            playerStats.lifeCount = data.lives - 1;
            playerStats.health = data.maxHp + 100;

            SaveGame.Save(playerStats);

            PlayerStats.death = false;
            Debug.Log("You died.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Campfire"))
            canSave = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Campfire"))
            canSave = false;
    }
}
