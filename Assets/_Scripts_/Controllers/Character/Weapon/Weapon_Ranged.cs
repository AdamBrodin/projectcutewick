using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Ranged : BaseWeapon
{
    [SerializeField] private float lastfired;
    [SerializeField] private float FireRate = 10;

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            if (Time.time - lastfired > 1 / FireRate)
            {
                lastfired = Time.time;
                Fire("Weapon_Ranged");
            }
        }
    }
}
