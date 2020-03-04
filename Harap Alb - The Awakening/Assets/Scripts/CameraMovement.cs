using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;

    Vector3 offset;

    private void Awake()
    {
        offset = - transform.position + player.transform.position;
    }

    private void LateUpdate()
    {
        transform.position = player.transform.position - offset;
    }
}
