using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public abstract class EnemyMovement : MonoBehaviour
{
    #region Variables
    [SerializeField] protected float moveSpeed;
    protected Rigidbody2D rgbd2d => GetComponent<Rigidbody2D>();
    #endregion

    protected abstract void Move();
    protected virtual void FixedUpdate() => Move();
}