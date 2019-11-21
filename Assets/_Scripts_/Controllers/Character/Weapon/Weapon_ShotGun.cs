using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_ShotGun : BaseWeapon
{
    [SerializeField] private float lastfired;
    [SerializeField] private float FireRate = 10;

    [SerializeField] private float shakeStrength, shakeDuration, shakeDecrease;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (Time.time - lastfired > 1 / FireRate)
            {
                lastfired = Time.time;
                Fire("Weapon_Shotgun");
                Fire("Weapon_Shotgun");
                Fire("Weapon_Shotgun");


                if (internal_Ammo > 0) CameraShake.Instance.ShakeCamera(shakeStrength, shakeDuration, shakeDecrease);
            }
        }
    }
}
