using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXP_Stats : MonoBehaviour
{
    public float rotateSpeed;
    public int points;

    void Update()
    {
        transform.Rotate(new Vector3(0f, 0f, rotateSpeed * Time.deltaTime));
    }
}
