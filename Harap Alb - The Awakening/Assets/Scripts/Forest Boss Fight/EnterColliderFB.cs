using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterColliderFB : MonoBehaviour
{
    public ForestBoss boss;
    public GameObject mainCamera;
    public GameObject bossCamera;

    bool activateFight = false;

    float timeUntilBossFightStarts = 6f;
    private void Update()
    {
        if (activateFight == false)
            return;

        if (timeUntilBossFightStarts <= 0f)
        {
            boss.SetBossActive();
            activateFight = false;
        }
        else
            timeUntilBossFightStarts -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        activateFight = true;
        timeUntilBossFightStarts = 6f;
        mainCamera.SetActive(false);
        bossCamera.SetActive(true);
    }
}
