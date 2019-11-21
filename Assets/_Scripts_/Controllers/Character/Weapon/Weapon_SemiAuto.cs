using UnityEngine;

public class Weapon_SemiAuto : BaseWeapon
{
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Fire("Weapon_Semi");
        }
    }
}
