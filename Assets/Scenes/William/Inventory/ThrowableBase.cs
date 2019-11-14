using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TrashBase : MonoBehaviour
{
    protected float damage;
    protected int health;
    protected bool canBreak;
    protected bool thrown; // On pickup set to false;
    public abstract void Break();

    protected void Update()
    {
        if (health <= 0)
            Break();
    }

    protected void Hit()
    {
        // Deal damage to the hitee
        health--;
    }

    protected void Throw()
    {
        // Throw to mouse pos
        // Quick throw
        thrown = true;
    }
}
