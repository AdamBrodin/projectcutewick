using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    public static Vector3 spawnpoint;

    void Start()
    {
        spawnpoint = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Checkpoint")
        {
            spawnpoint = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        }
    }
    void Respawn()
    {
        transform.position = spawnpoint;
    }
}
