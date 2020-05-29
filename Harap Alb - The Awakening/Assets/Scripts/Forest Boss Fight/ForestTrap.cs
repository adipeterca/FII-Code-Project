using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestTrap : MonoBehaviour
{
    public float secondsUntilDestroy;
    public float secondsToWait;
    public float downSpeed;

    private void Update()
    {
        if (secondsToWait > 0)
        {
            secondsToWait -= Time.deltaTime;
            return;
        }
        if (secondsUntilDestroy > 0)
        {
            transform.position -= new Vector3(0, downSpeed * Time.deltaTime, 0);
            secondsUntilDestroy -= Time.deltaTime;
        }
        else
            Destroy(gameObject);
    }
}
