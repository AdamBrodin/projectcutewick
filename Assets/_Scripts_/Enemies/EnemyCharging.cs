#pragma warning disable CS0649
using System.Collections;
using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public class EnemyCharging : EnemyPatrolling
{
    #region Variables
    [SerializeField] private float attackCooldown, chargeSpeed, knockbackForce;
    private bool onCooldown;
    #endregion

    protected override void Start()
    {
        base.Start();
    }

    protected override void AttackPlayer()
    {
        // TODO Play charge animation

        // Move the enemy towards the player after the animation is completed
        if (playerTransform != null && !onCooldown)
        {
            float step = Time.deltaTime * chargeSpeed;
            rgbd2d.position = Vector2.MoveTowards(rgbd2d.position, playerTransform.position, step);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" && !onCooldown)
        {
            // TODO Attack animation
            col.gameObject.GetComponent<Health>()?.ChangeHealth(-stats.damageValue);
            StartCoroutine(AttackCooldown());
            rgbd2d.AddRelativeForce(new Vector2(-knockbackForce, 0), ForceMode2D.Force);
        }
    }

    private IEnumerator AttackCooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(attackCooldown);
        onCooldown = false;
    }
}
