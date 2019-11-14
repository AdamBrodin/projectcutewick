#pragma warning disable CS0649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public Vector3 muzzleVelocity;

    public float TTL;

    public bool isBallistic;
    public float proj_Drag; // in metres/s lost per second.
    public float proj_Timer;

    [SerializeField] private int bulletDamage;

    void Update()
    {

        if (proj_Drag != 0)
            muzzleVelocity += muzzleVelocity * (-proj_Drag * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<Health>()?.ChangeHealth(-bulletDamage);
        }
        Destroy(gameObject);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
