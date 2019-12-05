using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    public int durability;
    public int hit_damage;
    public float obj_Mass;

    void Update()
    {
        if (durability < 1)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bullet_Basic")
        {
            Take_Damage(hit_damage);
        }
    }

    public void Take_Damage(int dmg)
    {
        durability -= dmg;
    } 
}
