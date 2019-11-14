using UnityEngine;

public class Weapon_SemiAuto : BaseWeapon
{
    protected override void Update()
    {
        base.Update();
        if (Input.GetButtonDown("Fire1"))
        {
            Fire("Weapon_Semi");
        }
    }
}
