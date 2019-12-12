using System.Collections;
using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public class EnemyFollowing : Enemy
{
    #region Variables
    protected GameObject playerObject;
    private bool isTouchingPlayer;

    [SerializeField] private float attackCooldown, chargeSpeed, knockbackForce;
    private bool onCooldown;
    #endregion

    protected override void Start()
    {
        base.Start();
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }
    protected override void Move() => FollowPlayer();
    protected void FollowPlayer()
    {
        rgbd2d.position = Vector2.MoveTowards(gameObject.transform.position, playerObject.transform.position, moveSpeed * Time.deltaTime);
    }

    protected override void AttackPlayer()
    {
        FollowPlayer();
        // TODO Play charge animation

        // Move the enemy towards the player after the animation is completed
        if (playerObject.transform != null && !onCooldown)
        {
            float step = Time.deltaTime * chargeSpeed;
            rgbd2d.position = Vector2.MoveTowards(rgbd2d.position, playerObject.transform.position, step);
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
