using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject camera;

    Vector3 offset;

    private void Awake()
    {
        offset = transform.position - camera.transform.position;
    }

    private void LateUpdate()
    {
        camera.transform.position = transform.position - offset;
    }
}
