using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    public GameObject minimap;

    static bool minimapActive = false;

    Vector3 offset;

    private void Awake()
    {
        offset = - transform.position + player.transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && PauseMenu.gameIsPaused == false && PlayerStats.levelUpMenuActive == false && playerIsIdle() == true)
        {
            if (minimapActive == false)
            {
                minimapActive = true;
                minimap.SetActive(true);
            }
            else
            {
                minimapActive = false;
                minimap.SetActive(false);
            }
        }
    }

    private void LateUpdate()
    {
        if (Movement.dontMoveCamera == true)
        {
            Movement.dontMoveCamera = false;
            offset += new Vector3(player.transform.localScale.x, 0, 0);
        }
        else
            transform.position = player.transform.position - offset;
    }

    static public bool GetMinimapStatus()
    {
        return minimapActive;
    }
    
    private bool playerIsIdle()
    {
        return player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Player_Idle");
    }
}
