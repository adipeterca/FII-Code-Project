using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    public float timeUntilDestroy = 5;
    public float fallSpeed = 1;
    public float rotateSpeed = 1;

    int directionOfRotation;

    Vector3 initPos;

    float timePassed = 0;

    private void Start()
    {
        if (Random.Range(0, 2) == 0)
            directionOfRotation = 1;
        else
            directionOfRotation = -1;

        timePassed = 0;
        initPos = transform.position;
    }

    private void Update()
    {
        transform.position -= new Vector3(0, Time.deltaTime * fallSpeed, 0);
        transform.Rotate(new Vector3(0, 0, directionOfRotation * Time.deltaTime * rotateSpeed));

        timePassed += Time.deltaTime;

        if (timePassed >= timeUntilDestroy)
        {
            transform.position = initPos;
            if (Random.Range(0, 2) == 0)
                directionOfRotation = 1;
            else
                directionOfRotation = -1;
            timePassed = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerStats>().TakeDamage(100000);
        }
    }
}
