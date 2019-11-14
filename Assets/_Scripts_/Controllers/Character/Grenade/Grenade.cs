using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public static float ammo;
    public GameObject grenade;
    public float grenadeSpeed;

    private void Update()
    {
         if (Input.GetButtonDown("Fire2") && ammo > 0)
         {
             ammo -= 1;
             Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
             Vector2 direction = worldMousePos - transform.position;
             
             direction.Normalize();
             Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
             GameObject projectile = Instantiate(grenade,
             transform.position + (Vector3)(direction * 0.5f),Quaternion.identity);
             projectile.GetComponent<Rigidbody2D>().velocity += direction * grenadeSpeed;
         }
    }
}
