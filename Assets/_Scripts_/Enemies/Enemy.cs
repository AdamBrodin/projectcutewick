using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public abstract class Enemy : Entity
{
    #region Variables
    protected Rigidbody2D rgbd2d => GetComponent<Rigidbody2D>();
    #endregion

    protected abstract void Move();
    protected abstract void AttackPlayer();
    protected virtual void FixedUpdate() => Move();
}
