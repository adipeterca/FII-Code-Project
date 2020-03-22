using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UI_Update : MonoBehaviour
{
    public PlayerStats player;
    public Text score;
    public Text health;
    public Text lives;

    private void Update()
    {
        score.text = player.points + " / 1000";
        health.text = player.health + " / " + (player.maxHealth + 100);
        lives.text = player.lifeCount.ToString();
    }
}
