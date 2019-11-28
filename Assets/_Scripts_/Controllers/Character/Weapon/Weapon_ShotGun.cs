using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_ShotGun : BaseWeapon
{
    [SerializeField] private float lastfired, cameraShakeAmount, cameraShakeDuration, cameraShakeDecrease;
    [SerializeField] private float FireRate = 10;

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
                CameraShake.Instance.ShakeCamera(cameraShakeAmount, cameraShakeDuration, cameraShakeDecrease);
            }
        }
    }
}
