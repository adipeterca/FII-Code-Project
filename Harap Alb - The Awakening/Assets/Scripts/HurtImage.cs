using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HurtImage : MonoBehaviour
{
    const float timeToDisplay = 0.1f;
    float timeRemaining;

    private void Update()
    {

        if (timeRemaining <= 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            timeRemaining -= Time.deltaTime;
        }
    }

    private void OnEnable()
    {
        timeRemaining = timeToDisplay;
    }
}
