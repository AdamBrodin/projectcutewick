#pragma warning disable CS0649
using System.Collections;
using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public class EnemyPatrolling : EnemyFollowing
{
    #region Variables
    private int direction;
    [SerializeField] private float minSideSwitch, maxSideSwitch, minIdleTime, maxIdleTime, idleChance, idleSlowDownTime, followProximity, attackProximity;
    private Vector2 startScale => transform.localScale;

    private enum EnemyState
    {
        Idle,
        Patrolling,
        Following,
        Attacking
    }
    private EnemyState currentState = EnemyState.Patrolling;
    #endregion

    protected override void Start()
    {
        base.Start();
        ChangeDirection();
        StartCoroutine(SideSwitch());
    }
    private void Update() => StateUpdate();
    protected override void Move()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                rgbd2d.velocity = Vector2.Lerp(new Vector2(rgbd2d.velocity.x, rgbd2d.velocity.y), new Vector2(0, 0), idleSlowDownTime);
                break;
            case EnemyState.Patrolling:
                rgbd2d.velocity = new Vector2(direction * moveSpeed, rgbd2d.velocity.y);
                break;
            case EnemyState.Following:
                FollowPlayer();
                break;
            case EnemyState.Attacking:
                AttackPlayer();
                break;
        }
    }

    private IEnumerator SideSwitch()
    {
        float chance = Random.Range(0, 100);
        if (idleChance >= chance)
        {
            currentState = EnemyState.Idle;
            yield return new WaitForSeconds(Random.Range(minIdleTime, maxIdleTime));
            currentState = EnemyState.Patrolling;
        }
        else { ChangeDirection(); }
        yield return new WaitForSeconds(Random.Range(minSideSwitch, maxSideSwitch));
        StartCoroutine(SideSwitch());
    }

    private void ChangeDirection()
    {
        if (direction == 0) { direction = Random.Range(-1, 1); }
        else { direction *= -1; }

        if (rgbd2d.velocity.x <= 0)
        {
            transform.localScale = new Vector2(-startScale.x, transform.localScale.y);
        }
        else
        {
            transform.localScale = new Vector2(startScale.x, transform.localScale.y);
        }

        /*
                bool turnRight = direction > 0;
        float newScale = 0;
        if (turnRight)
        {
            newScale = startScale.x;
        }
        else
        {
            newScale = startScale.x * -1;
        }
        transform.localScale = new Vector2(newScale, startScale.y);
        */
    }

    private void StateUpdate()
    {
        float distanceToPlayer = Vector2.Distance(gameObject.transform.position, playerTransform.position);
        if (distanceToPlayer <= followProximity)
        {
            if (distanceToPlayer <= attackProximity)
            {
                currentState = EnemyState.Attacking;
            }
            else
            {
                currentState = EnemyState.Following;
            }
        }
        else if (currentState != EnemyState.Idle)
        {
            currentState = EnemyState.Patrolling;
        }
    }
}

