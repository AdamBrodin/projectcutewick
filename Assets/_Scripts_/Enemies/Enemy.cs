using System;
using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public abstract class Enemy : Entity
{
    #region Singleton
    private static Enemy instance;
    public static Enemy Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Enemy>();
            }
            return instance;
        }
    }
    #endregion
    #region Variables
    protected Rigidbody2D rgbd2d => GetComponent<Rigidbody2D>();
    public Action OnEnemyDeath;
    #endregion

    protected abstract void AttackPlayer();
    protected override void OnDeathEffect() => OnEnemyDeath?.Invoke();
    protected abstract void Move();
    protected virtual void FixedUpdate() => Move();

}
