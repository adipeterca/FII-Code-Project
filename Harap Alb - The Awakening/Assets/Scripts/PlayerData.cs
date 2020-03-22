using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {
    
    // Maximum health of the player.
    public int maxHp;
    // The attack power skill of the player.
    public int atkPower;
    // The speed of the player.
    public int speed;
    // The number of lives remaining.
    public int lives;
    // Current health.
    public int currentHp;
    // Number of points.
    public int points;
    // The position for our player.
    public float[] position;

    // Default constructor.
    public PlayerData(PlayerStats playerReference)
    {
        position = new float[3];
        position[0] = playerReference.position[0];
        position[1] = playerReference.position[1];
        position[2] = playerReference.position[2];

        maxHp = playerReference.maxHealth;
        atkPower = playerReference.atkPower;
        speed = playerReference.speed;

        lives = playerReference.lifeCount;
        currentHp = playerReference.health;

        points = playerReference.points;
    }
}
