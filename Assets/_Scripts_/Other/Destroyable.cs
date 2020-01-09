using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    public int durability;
    public int hit_damage;
    public float obj_Mass;

    private void Update()
    {
        if (durability < 1)
        {
            DestroySelf();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bullet_Basic")
        {
            Take_Damage(hit_damage);
        }
    }

    protected virtual void DestroySelf() { }

    public void Take_Damage(int dmg)
    {
        durability -= dmg;
    }
}
