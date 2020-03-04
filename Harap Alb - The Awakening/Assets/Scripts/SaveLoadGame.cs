using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadGame : MonoBehaviour
{
    //
    public PlayerStats playerStats;

    // Can the player save? (Is him near a campfire?)
    bool canSave = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && canSave)
        {
            SaveGame.Save(playerStats);
            canSave = false;
            Debug.Log("Saved game.");
        }
        else if(Input.GetKeyDown(KeyCode.L))
        {
            PlayerData data = SaveGame.Load();
            transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
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
