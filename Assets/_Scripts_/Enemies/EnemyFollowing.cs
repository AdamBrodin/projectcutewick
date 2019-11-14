using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public class EnemyFollowing : Enemy
{
    #region Variables
    protected Transform playerTransform;
    #endregion

    protected override void Start()
    {
        base.Start();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    protected override void Move() => FollowPlayer();
    protected void FollowPlayer()
    {
        rgbd2d.position = Vector2.MoveTowards(gameObject.transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
    }

    protected override void AttackPlayer()
    {
        FollowPlayer();
        // Attack stuff
    }
}
