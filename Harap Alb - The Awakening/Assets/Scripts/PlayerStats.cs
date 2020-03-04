using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // The position of the player.
    public float[] position;
    // Skills: max health, attack power and speed.
    public int maxHealth;
    public int atkPower;
    public int speed;   // this needs to be the same as the one in Movement script. -------------TO DO-----------------
    // Currect health.
    public int health;
    // Lives counter.
    public int lifeCount;
    // Points counter.
    public int points;  // needs a function called addPoints(int amount) which also check to see if the necessary points 
                        // (default 1000) to level up have been collected.
                        // This needs to be done until points < 1000. (A boss could give the player 3000 points)
    private void Awake()
    {
        position = new float[3];
    }

    /* public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <=0) 
        {
            lifeCount--;
            SaveGame.Load();
        }
    }*/

    void Update()
    {
        if (lifeCount >= 0)
        {
            position[0] = transform.position.x;
            position[1] = transform.position.y;
            position[2] = transform.position.z;


        }
        // else EndGame();
    }
}
