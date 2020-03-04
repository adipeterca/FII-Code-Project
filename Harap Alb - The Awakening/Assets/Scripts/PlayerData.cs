using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {
    
    // Maximum health of the player.
    public int maxHp;
    // The power skill of the player.
    public float power;
    // The speed of the player.
    public float speed;
    // The number of lives remaining.
    public int lives;
    // Current health.
    public int currentHp;
    // The position for our player.
    public float[] position;
    // Is our player facing right?
    public bool isFacingRight;

    // Default constructor.
    public PlayerData(PlayerStats playerReference)
    {
        position = new float[3];
        position[0] = playerReference.position[0];
        position[1] = playerReference.position[1];
        position[2] = playerReference.position[2];
    }
}
